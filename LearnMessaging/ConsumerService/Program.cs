using ConsumerService.Listeners;

var listener = new OrdersListener(new Logic.RabbitMq.RabbitMqClient());
listener.ProcessQueues();