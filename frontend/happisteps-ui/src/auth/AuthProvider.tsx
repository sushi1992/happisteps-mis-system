import { useState } from "react"
import { AuthContext } from "./AuthContext"

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [token, setTokenState] = useState<string | null>(() =>
    localStorage.getItem("auth.token")
  )

  const [ready, setReady] = useState(false)

  const setToken = (token: string | null) => {
    if (token) {
      localStorage.setItem("auth.token", token)
    } else {
      localStorage.removeItem("auth.token")
    }
    setTokenState(token)
    setReady(true)
  }

  return (
    <AuthContext.Provider value={{ token, setToken, ready }}>
      {children}
    </AuthContext.Provider>
  )
}
