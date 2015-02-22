﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.impl;
using EntityValidator.exeptions;

namespace EntityValidator.rules
{
    public class OneStartRule : IRule
    {
        private List<Entity> starts = new List<Entity>();
        private List<Entity> allEntities = Model.Instance.getEntities();

        public Boolean validate()
        {
            if (allEntities == null || allEntities.Count == 0)
                return false;

            foreach (Entity entity in allEntities)
            {
                if (entity is EntityStart)
                    starts.Add(entity);
            }

            return starts.Count == 1;
        }

        public List<ValidationError> explain()
        {
            List<ValidationError> errors = new List<ValidationError>();
            if (starts.Count == 0)
            {
                errors.Add(new ValidationError("Сущность 'Старт' не найдена"));
            }
            else if (starts.Count > 1)
            {
                foreach (Entity start in starts)
                {
                    errors.Add(new ValidationError("Лишняя сущность 'Старт'", start));
                }
            }
            return errors;
        }
    }
}
