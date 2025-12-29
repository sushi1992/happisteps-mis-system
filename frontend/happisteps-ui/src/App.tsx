import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { ChildrenList } from "./pages/ChildrenList";
import { ChildDetails } from "./pages/ChildDetails";
import { OnRollRegister } from "./pages/OnRollRegister";
import { useDevLogin } from "./auth/useDevLogin"


export default function App() {
  useDevLogin() 
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to="/children" />} />
        <Route path="/children" element={<ChildrenList />} />
        <Route path="/children/:id" element={<ChildDetails />} />
        <Route path="/registers/on-roll" element={<OnRollRegister />} />
      </Routes>
    </BrowserRouter>
  );
}
