const http = require('http');

module.exports = function (context, myQueueItem) {
  return new Promise((resolve, reject) => {
    
    var id = Math.floor(Math.random() * 10000000)
    let req = http.request(
        {
          host: 'fireworks-web.default.svc.cluster.local',
          path: '/api/values?type=' + myQueueItem + '&value=v1&id=' + id,
          method: 'POST'
        },
        res => {
          let buffers = [];
          res.on('error', reject);
          res.on('data', buffer => buffers.push(buffer));
          res.on(
            'end',
            () =>
              res.statusCode === 200
                ? resolve(Buffer.concat(buffers))
                : reject(Buffer.concat(buffers))
          );
        }
      );
      req.end();
  })
};