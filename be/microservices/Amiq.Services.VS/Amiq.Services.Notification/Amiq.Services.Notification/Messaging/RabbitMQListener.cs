using Amiq.Services.Common.Enums;
using Amiq.Services.Notification.Contracts.User;
using Amiq.Services.Notification.DataAccessLayer.Models;
using Amiq.Services.Notification.Messaging.IntegrationEvents;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Amiq.Services.Notification.Messaging
{
    public class RabbitMQListener : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;
        private AmiqNotificationContext _amiqNotificationContext;

        public RabbitMQListener()
        {
            _amiqNotificationContext = new AmiqNotificationContext();

            _connection = RabbitMQConnectionFactory.Factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout, true);
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName,
                exchange: "trigger",
                routingKey: "");

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {

        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                string message = Encoding.UTF8.GetString(body.ToArray());

                _channel.BasicAck(ea.DeliveryTag, false);

                var integrationEvent = JsonSerializer.Deserialize<IntegrationEvent>(message);
                switch (integrationEvent.EventName)
                {
                    case nameof(UserModificationEvent):
                        {
                            var @event = JsonSerializer.Deserialize<UserModificationEvent>(message);
                            var dtoBasicUserInfo = new DtoBasicUserInfo
                            {
                                UserId = @event.UserId,
                                Name = @event.Name,
                                Surname = @event.Surname,
                                AvatarPath = @event.AvatarPath
                            };
                            var user = _amiqNotificationContext.Users.Find(dtoBasicUserInfo.UserId);
                            if (user == null)
                            {
                                _amiqNotificationContext.Users.Add(new User
                                {
                                    UserId = dtoBasicUserInfo.UserId,
                                    Name = dtoBasicUserInfo.Name,
                                    Surname = dtoBasicUserInfo.Surname,
                                    AvatarPath = dtoBasicUserInfo.AvatarPath
                                });
                            }
                            else
                            {
                                user.Name = dtoBasicUserInfo.Name;
                                user.Surname = dtoBasicUserInfo.Surname;
                                user.AvatarPath = dtoBasicUserInfo.AvatarPath;
                            }

                            break;
                        }
                    case nameof(FriendshipModificationEvent):
                        {
                            var @event = JsonSerializer.Deserialize<FriendshipModificationEvent>(message);
                            var eventAction = EnumExtensions.GetValueByAlt<EnIntegrationEventAction>(@event.Action);

                            switch (eventAction)
                            {
                                case EnIntegrationEventAction.Created:
                                    {
                                        _amiqNotificationContext.Friendships.Add(new Friendship { 
                                            FriendshipId = @event.FriendshipId,
                                            FirstUserId = @event.FirstUserId,
                                            SecondUserId = @event.SecondUserId
                                        });
                                        break;
                                    }
                                case EnIntegrationEventAction.Removed:
                                    {
                                        var entity = _amiqNotificationContext.Friendships.Find(@event.FriendshipId);
                                        if(entity != null)
                                        {
                                            _amiqNotificationContext.Friendships.Remove(entity);
                                        }
                                        break;
                                    }
                                case EnIntegrationEventAction.Edited:
                                    {
                                        var entity = _amiqNotificationContext.Friendships.Find(@event.FriendshipId);
                                        if (entity != null)
                                        {
                                            entity.FirstUserId = @event.FirstUserId;
                                            entity.SecondUserId = @event.SecondUserId;
                                        }
                                        break;
                                    }
                            }

                            break;
                        }
                    case nameof(GroupParticipantModificationEvent): 
                        {
                            var @event = JsonSerializer.Deserialize<GroupParticipantModificationEvent>(message);
                            var eventAction = EnumExtensions.GetValueByAlt<EnIntegrationEventAction>(@event.Action);

                            switch (eventAction)
                            {
                                case EnIntegrationEventAction.Created:
                                    {
                                        _amiqNotificationContext.GroupParticipants.Add(new GroupParticipant { 
                                            GroupParticipantId = @event.GroupParticipantId,
                                            GroupId = @event.GroupId,
                                            UserId = @event.UserId
                                        });
                                        break;
                                    }
                                case EnIntegrationEventAction.Removed:
                                    {
                                        var entity = _amiqNotificationContext.GroupParticipants.Find(@event.GroupParticipantId);
                                        if (entity != null)
                                        {
                                            _amiqNotificationContext.GroupParticipants.Remove(entity);
                                        }
                                        break;
                                    }
                                case EnIntegrationEventAction.Edited:
                                    {
                                        var entity = _amiqNotificationContext.GroupParticipants.Find(@event.GroupParticipantId);
                                        if (entity != null)
                                        {
                                            entity.GroupParticipantId = @event.GroupParticipantId;
                                            entity.GroupId = @event.GroupId;
                                            entity.UserId = @event.UserId;
                                        }
                                        break;
                                    }
                            }

                            break;
                        }
                    case nameof(GroupPostModificationEvent):
                        {
                            var @event = JsonSerializer.Deserialize<GroupPostModificationEvent>(message);
                            var eventAction = EnumExtensions.GetValueByAlt<EnIntegrationEventAction>(@event.Action);

                            switch (eventAction)
                            {
                                case EnIntegrationEventAction.Created:
                                    {
                                        _amiqNotificationContext.GroupPosts.Add(new GroupPost { 
                                            GroupPostId = @event.GroupPostId,
                                            PostId = @event.PostId,
                                            TextContent = @event.TextContent,
                                            EditedBy = @event.EditedBy,
                                            EditedAt = @event.EditedAt,
                                            CreatedAt = @event.CreatedAt,
                                            GroupId = @event.GroupId,
                                            AuthorId = @event.AuthorId,
                                            VisibleAsCreatedByAdmin = @event.VisibleAsCreatedByAdmin
                                        });
                                        break;
                                    }
                            }

                            break;
                        }
                    case nameof(UserPostModificationEvent):
                        {

                            var @event = JsonSerializer.Deserialize<UserPostModificationEvent>(message);
                            var eventAction = EnumExtensions.GetValueByAlt<EnIntegrationEventAction>(@event.Action);

                            switch (eventAction)
                            {
                                case EnIntegrationEventAction.Created:
                                    {
                                        _amiqNotificationContext.UserPosts.Add(new UserPost
                                        {
                                            UserPostId = @event.UserPostId,
                                            PostId = @event.PostId,
                                            TextContent = @event.TextContent,
                                            EditedBy = @event.EditedBy,
                                            EditedAt = @event.EditedAt,
                                            CreatedAt = @event.CreatedAt,
                                            UserId = @event.UserId
                                        });
                                        break;
                                    }
                            }

                            break;
                        }
                }

                _amiqNotificationContext.SaveChanges();
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(queue: _queueName, consumer: consumer);

            return Task.CompletedTask;
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
