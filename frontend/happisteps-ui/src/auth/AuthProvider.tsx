import { useState } from "react"
import { AuthContext } from "./AuthContext"

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [token, setTokenState] = useState<string | null>(() =>
    localStorage.getItem("auth.token")
  )

  const setToken = (token: string | null) => {
    if (token) {
      localStorage.setItem("auth.token", token)
    } else {
      localStorage.removeItem("auth.token")
    }

    setTokenState(token)
  }

  const logout = () => {
    localStorage.removeItem("auth.token")
    setTokenState(null)
  }

  return (
    <AuthContext.Provider
      value={{
        token,
        setToken,
        logout,
        ready: true // ✅ always ready
      }}
    >
      {children}
    </AuthContext.Provider>
  )
}

