import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { ChildrenList } from "./pages/ChildrenList";
import { ChildDetails } from "./pages/ChildDetails";
import { OnRollRegister } from "./pages/OnRollRegister";
import { Login } from "./pages/Login"
import { useContext } from "react"
import { AuthContext } from "./auth/AuthContext"

export default function App() {
  const { token, ready, logout } = useContext(AuthContext)

  if (!ready) return null

  if (!token) {
    return <Login />
  }

  return (
    <>
      <header style={{ padding: 12 }}>
        <button onClick={logout}>Log out</button>
      </header>

      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Navigate to="/children" />} />
          <Route path="/children" element={<ChildrenList />} />
          <Route path="/children/:id" element={<ChildDetails />} />
          <Route path="/registers/on-roll" element={<OnRollRegister />} />
        </Routes>
      </BrowserRouter>
    </>
  )
}
