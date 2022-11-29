const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/api",
    "/api/auth",
    "/api/tracks",
    "/api/pilot",
    "/api/team",
    "/api/stages",
    "/api/results",
    "/api/manufacturer",
    "/api/reset"
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:7052',
        secure: false
    });

    app.use(appProxy);
};
