import { msal } from "./msal"

export async function loginWithMicrosoft(): Promise<string> {
  const result = await msal.loginPopup({
    scopes: ["openid", "profile", "email"]
  })

  if (!result.code)
    throw new Error("Microsoft did not return an auth code")

  return result.code
}
