import { createContext } from "react"

export interface AuthContextValue {
  token: string | null
  setToken: (token: string | null) => void
  logout: () => void
  ready: boolean
}

export const AuthContext = createContext<AuthContextValue>({
  token: null,
  setToken: () => {},
  logout: () => {},
  ready: false
})
