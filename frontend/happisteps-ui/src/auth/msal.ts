import { PublicClientApplication } from "@azure/msal-browser"

export const msal = new PublicClientApplication({
  auth: {
    clientId: "cc125831-ea61-48c1-bee7-5571a0ed2aab",
    authority: "https://login.microsoftonline.com/1f142ba3-9321-405f-bee5-e8f01d291998",
    redirectUri: "http://localhost:5173"
  }
})

export const msalReady = msal.initialize()