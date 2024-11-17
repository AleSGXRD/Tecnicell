const { env } = require('process');

/* const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7193';
 */
const target = 'http://192.168.86.177:5000'
const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
      "/api",
      "^/api/(.*)$"
    ],
    target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;
