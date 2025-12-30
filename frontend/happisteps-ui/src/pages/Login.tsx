import { loginWithMicrosoft } from "../auth/loginWithMicrosoft"
import { useContext } from "react"
import { AuthContext } from "../auth/AuthContext"

export function Login() {
  const { setToken } = useContext(AuthContext)

  const handleMicrosoftLogin = async () => {
    const idToken = await loginWithMicrosoft()

    const res = await fetch(
      "http://localhost:5209/api/auth/microsoft/exchange",
      {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ idToken })
      }
    )

    if (!res.ok)
      throw new Error("Microsoft exchange failed")

    const { token } = await res.json()
    setToken(token)
  }

  return (
    <button onClick={handleMicrosoftLogin}>
      Sign in with Microsoft
    </button>
  )
}
