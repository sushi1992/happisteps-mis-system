import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { ChildrenList } from "./pages/ChildrenList";
import { ChildDetails } from "./pages/ChildDetails";
import { OnRollRegister } from "./pages/OnRollRegister";
import { Login } from "./pages/Login"
import { AuthContext } from "./auth/AuthContext"
import { useContext } from "react"

export default function App() {
  const { token, ready } = useContext(AuthContext);

  if (!ready) return null;

  if (!token) {
    return <Login />;
  }

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to="/children" />} />
        <Route path="/children" element={<ChildrenList />} />
        <Route path="/children/:id" element={<ChildDetails />} />
        <Route path="/registers/on-roll" element={<OnRollRegister />} />
      </Routes>
    </BrowserRouter>
  )
}
