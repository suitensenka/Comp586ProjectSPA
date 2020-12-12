import { domain, clientId, audience, apiUrl } from ".../../auth_config.json"; 

export const environment = {
  production: true,
  auth: {
    domain,
    clientId,
    redirectUri: window.location.origin,
    audience,
  },
  httpInterceptor: {
    allowedList: [{uri: "https://spaserver.comp586spaclient.xyz:81/*"}],
  },

  dev: {
    apiUrl,
  },
};
