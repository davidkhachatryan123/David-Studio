export const environment = {
  production: true,
  identity: {
    authority: 'https://$HOST:5011',
    scopes: [
      'openid', 'profile', 'offline_access',
      'users', 'portfolio', 'pricing', 'storage', 'messenger'
    ]
  },
  api: 'https://$HOST:8084/api/v1'
};
