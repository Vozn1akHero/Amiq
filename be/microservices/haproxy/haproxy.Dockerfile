FROM haproxy:1.7.1
COPY haproxy.config /usr/local/etc/haproxy/haproxy.cfg

EXPOSE 80