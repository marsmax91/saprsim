﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.impl
{
    public class Parallel : Entity
    {

        public override void execute() {
            Project project = getProjectFromReadyQueue();

            if (project != null)
            {
                Project prj = getProjectFromReadyQueue();

                Entity outputEntity1 = getOutputs()[0];
                outputEntity1.addProjectToQueue(prj);

                Entity outputEntity2 = getOutputs()[1];
                outputEntity2.addProjectToQueue(prj);

                this.getReadyProjectQueue().Remove(prj);
            }
        }

        public override bool canUseAsInput(Entity entity)
        {
            return entity is Procedure || entity is Synchronization || entity is Parallel || entity is EntityStart;
        }

        public override bool canUseAsOutput(Entity entity)
        {
            return entity is Procedure || entity is Synchronization || entity is Parallel || entity is EntityDestination;
        }

        public override bool correctInputCount()
        {
            return input.Count == 1;
        }

        public override bool correctOutputCount()
        {
            return output.Count == 2;
        }
    }
}
