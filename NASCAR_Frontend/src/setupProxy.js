const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/api",
    "/api/auth",
    "/api/tracks",
    "/api/addresult",
    "/api/addresult/configure",
    "/api/pilot"
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:7052',
        secure: false
    });

    app.use(appProxy);
};
