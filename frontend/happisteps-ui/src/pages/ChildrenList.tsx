import { useEffect, useState, useContext } from "react"
import { api } from "../api/http"
import type { Child } from "../types/Child"
import { AuthContext } from "../auth/AuthContext"

export function ChildrenList() {
  const { ready } = useContext(AuthContext)
  const [children, setChildren] = useState<Child[]>([])

  useEffect(() => {
    if (!ready) 
      return

    api<Child[]>("/api/children")
      .then(setChildren)
      .catch(err => {
        console.error(err)
        alert("API call failed - likely auth")
      })
  }, [ready])

  if (!ready) {
    return <div>Loading session…</div>
  }

  return (
    <>
      <h1>Children</h1>
      <pre>{JSON.stringify(children, null, 2)}</pre>
    </>
  )
}
