import { createContext } from "react"

export interface AuthContextValue {
  token: string | null
  setToken: (token: string | null) => void
  ready: boolean
}

export const AuthContext = createContext<AuthContextValue>({
  token: null,
  setToken: () => {},
  ready: false
})
