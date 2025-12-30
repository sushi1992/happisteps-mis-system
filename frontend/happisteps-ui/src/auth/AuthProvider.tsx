import { useEffect, useState } from "react"
import { AuthContext } from "./AuthContext"
import { msal, msalReady } from "./msal"
import { api } from "../api/http"

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [token, setTokenState] = useState<string | null>(() =>
    localStorage.getItem("auth.token")
  )
  
  const [checking, setChecking] = useState(() => !!token)

  // Validate persisted auth token once on app startup.
  // We intentionally do NOT re-run this when `token` changes
  // (new tokens are trusted when set via login).
  useEffect(() => {
    if (!token) return

    api("/api/auth/me")
      .then(() => setChecking(false))
      .catch(() => {
        localStorage.removeItem("auth.token")
        setTokenState(null)
        setChecking(false)
      })
  }, [])

  const setToken = (token: string | null) => {
    if (token) {
      localStorage.setItem("auth.token", token)
    } else {
      localStorage.removeItem("auth.token")
    }
    setTokenState(token)
  }

  const logout = async () => {
    await msalReady

    await msal.logoutPopup({
      postLogoutRedirectUri: window.location.origin
    })

    localStorage.removeItem("auth.token")
    setTokenState(null)
  }

  return (
    <AuthContext.Provider
      value={{
        token,
        setToken,
        logout,
        ready: !checking
      }}
    >
      {children}
    </AuthContext.Provider>
  )
}
