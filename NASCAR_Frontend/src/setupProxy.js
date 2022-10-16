const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/api/wf",
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:7052',
        secure: false
    });

    app.use(appProxy);
};
