export const environment = {
  production: true,
  identity: {
    authority: 'https://localhost:5011',
    scopes: [
      'openid', 'profile', 'offline_access',
      'users', 'portfolio', 'pricing', 'storage', 'messenger'
    ]
  },
  api: 'https://localhost:8084/api/v1'
};
