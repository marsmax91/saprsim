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
    public class InputAndOutputNotEmptyRule : Rule
    {

        private List<Entity> failed = new List<Entity>();

        public InputAndOutputNotEmptyRule(List<Entity> entities) : base(entities)
        {
        }

        public override bool validate()
        {
            if (entities == null || entities.Count() == 0)
                return false;

            foreach (Entity entity in entities)
            {                  
                if (!(entity.correctInputCount() && entity.correctOutputCount()))
                    failed.Add(entity);
                if (entity is Procedure)
                {
                    Procedure p = entity as Procedure;
                    if (p.getResources().Count == 0)
                        failed.Add(entity);
                }
            }
            return failed.Count == 0;
        }

        public override List<ValidationError> explain()
        {
            List<ValidationError> errors = new List<ValidationError>();
            foreach (Entity fail in failed)
            {
                errors.Add(new ValidationError("Сущность '" + fail.ToString() + "' не верно подключена", fail));
            }
            return errors;
        }

    }
}
