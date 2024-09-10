﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class EventConstants
    {
        public const string CREATE_PRODUCTION_EXCHANGE = "CREATE_PRODUCTION_EXCHANGE";
        public const string CREATE_PRODUCTION_QUEUE = "CREATE_PRODUCTION_QUEUE";

        public const string START_PRODUCTION_EXCHANGE = "START_PRODUCTION_EXCHANGE";
        public const string START_PRODUCTION_QUEUE = "PRODUCTION_START_PRODUCTION_QUEUE";

        public const string CANCEL_PRODUCTION_EXCHANGE = "CANCEL_PRODUCTION_EXCHANGE";
        public const string CANCEL_PRODUCTION_QUEUE = "CANCEL_PRODUCTION_QUEUE";

        public const string FINISH_PRODUCTION_EXCHANGE = "FINISH_PRODUCTION_EXCHANGE";
        public const string FINISH_PRODUCTION_QUEUE = "PRODUCTION_FINISH_PRODUCTION_QUEUE";
    }
}
