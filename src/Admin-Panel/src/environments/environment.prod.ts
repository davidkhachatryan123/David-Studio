export const environment = {
  production: true,
  server: {
    protocol: 'http',
    domain: 'localhost',
    api_uri: '/api',
  },
  sessionStorageConfig: {
    USER_KEY: 'auth-user',
    TOKEN_KEY: 'token'
  }
};
