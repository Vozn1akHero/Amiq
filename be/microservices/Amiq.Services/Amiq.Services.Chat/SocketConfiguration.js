import { Server } from "socket.io";

export default class SocketConfiguration {
    static io;

    static init = (server, port) => {
        this.io = new Server(server);
        this.io.listen(port);
        this.io.on('connection', () => {
            console.log("socket.io connection works")
        });
    }
}

