﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PrakharWcfService45
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfService.svc or WcfService.svc.cs at the Solution Explorer and start debugging.
    public class WcfService : IWcfService
    {
        public List<TR_Agent> AgentData()
        {
            AgentDBEntities db = new AgentDBEntities();
            return db.TR_Agent.ToList();
        }

    }
}
