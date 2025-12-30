import { useContext, useEffect } from "react"
import { AuthContext } from "./AuthContext"

export function useDevLogin() {
  const { token, setToken } = useContext(AuthContext)

  useEffect(() => {
    if (import.meta.env.MODE !== "development") return
    if (token) return

    fetch(
      "http://localhost:5209/api/auth/dev-login?userId=11111111-1111-1111-1111-111111111111&organisationId=22222222-2222-2222-2222-222222222222"
    )
      .then(r => r.json())
      .then(data => setToken(data.token))
      .catch(console.error)
  }, [token, setToken])
}
