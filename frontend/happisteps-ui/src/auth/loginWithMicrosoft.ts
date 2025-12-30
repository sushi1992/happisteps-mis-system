import { msal, msalReady } from "./msal"

export async function loginWithMicrosoft(): Promise<string> {
  await msalReady

  const result = await msal.loginPopup({
    scopes: ["openid", "profile", "email"]
  })

  if (!result.idToken) {
    throw new Error("Microsoft did not return an ID token")
  }

  return result.idToken
}
