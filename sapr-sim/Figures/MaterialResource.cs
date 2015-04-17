﻿using sapr_sim.Parameters;
using sapr_sim.Utils;
using sapr_sim.WPFCustomElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace sapr_sim.Figures
{
    [Serializable]
    public class MaterialResource : Resource, ISerializable
    {

        private UIParam<int> consumption = new UIParam<int>(1, new IntegerParamValidator(), "Расход");
        private UIParam<TimeMeasure> consumptionTimeMeasure = new UIParam<TimeMeasure>(TimeMeasure.SECOND, new DefaultParamValidator(), "за", new ParameterComboBox(TimeMeasure.list()));

        public MaterialResource(Canvas canvas) : base(canvas)
        {
            init();
            textParam.Value = ResourceType.MATERIAL.Name;
            type.Value = ResourceType.MATERIAL;
        }

        public MaterialResource(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            consumption = info.GetValue("consumption", typeof(UIParam<int>)) as UIParam<int>;

            consumptionTimeMeasure = info.GetValue("consumptionTimeMeasure", typeof(UIParam<TimeMeasure>)) as UIParam<TimeMeasure>;
            consumptionTimeMeasure.ContentControl = new ParameterComboBox(TimeMeasure.list()) { SelectedIndex = consumptionTimeMeasure.Value.Order };

            init();
        }

        public override void createAndDraw(double x, double y)
        {
            base.createAndDraw(x, y);
            label = new Label(this, canvas, x + 20, y + 22, textParam.Value);
            canvas.Children.Add(label);
        }

        public override List<UIParam> getParams()
        {
            List<UIParam> param = base.getParams();
            param.Add(consumption);
            param.Add(consumptionTimeMeasure);
            return param;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("consumption", consumption);
            info.AddValue("consumptionTimeMeasure", consumptionTimeMeasure);
        }

        protected override void init()
        {
            base.init();
            Fill = Brushes.LightSeaGreen;
        }
    }
}
