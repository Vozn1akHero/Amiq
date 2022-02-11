import amqplib from 'amqplib'
import User from "../models/UserModel";
import Chat from "../models/ChatModel";
import Message from "../models/MessageModel";

const UserModificationEvent = 'UserModificationEvent'
const FriendshipCreated = 'FriendshipCreated'


const initializeRabbitMQListener = async () => {
    const connection = await amqplib.connect({
        protocol: "amqp",
        hostname: "host.docker.internal",
        port: "5672",
        username: "sa",
        password: "123dimon"
    }, {
        //heartbeat: "60"
    });

    console.log("CONNECTED TO RABBITMQ")

    let channel = await connection.createChannel()

    await channel.assertExchange('trigger', 'fanout', {routingKey: ""});

    const q = await channel.assertQueue()

    await channel.bindQueue(q.queue, 'trigger', '')

    await channel.consume(q.queue, async (msg) => {
        const msgAsJson = JSON.parse(msg.content);

        switch (msgAsJson.eventName) {
            case UserModificationEvent: {
                const {userId, name, surname, avatarPath} = msgAsJson;
                //console.log(msgAsJson)
                User.findByIdAndUpdate(
                    userId,
                    {$set: {_id: userId, name, surname, avatarPath}},
                    {
                        new: true,
                        upsert: true,
                        useFindAndModify: false
                    },
                    (err, doc) => {
                        if (err) {
                            console.log(err);
                        }
                        console.log(doc);
                    });
                break;
            }
            case FriendshipCreated: {
                const {issuerId, receiverId} = JSON.parse(msg.content);

                const message = await Message.create({
                    textContent: "Cześć",
                    author: issuerId
                })

                await Chat.create({
                    users: [issuerId, receiverId],
                    messages: [message._id]
                })
            }
        }

        //console.log(msg.content.toString())
        channel.ack(msg)
    })
}

export default initializeRabbitMQListener;