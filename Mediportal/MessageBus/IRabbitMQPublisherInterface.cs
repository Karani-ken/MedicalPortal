﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBus
{
    public interface IRabbitMQPublisherInterface
    {
        void PublishMessage(object message, string queue_topic_name);
    }
}
