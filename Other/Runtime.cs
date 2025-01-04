using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProLabHazine
{
    public class Runtime
    {
        public static int totalStepValue;
        public static int CollectedTresuresvalue;
        private List<Tresure> tresures;
        AnaForm anaForm;

        public Runtime(List<Tresure> _tresures, AnaForm _anaForm)
        {

            tresures = _tresures;
            anaForm = _anaForm;
        }

        public void CollectTresures(int nextXLocation, int nextYLocation)
        {

            foreach (Tresure tresure in tresures)
                if (tresure.Bounds.Contains(nextXLocation, nextYLocation) || tresure.Bounds.Contains(nextXLocation, nextYLocation) ||
                    tresure.Bounds.Contains(nextXLocation, nextYLocation) || tresure.Bounds.Contains(nextXLocation, nextYLocation))
                {
                    string obstacleInfo = $"tresure found:{tresure.Location.XLocation},{tresure.Location.YLocation},{tresure.tresureName} ";
                    if (!anaForm.lstBoxGameObjects.Items.Contains(obstacleInfo))
                    {
                        anaForm.lstBoxGameObjects.Items.Add(obstacleInfo);
                        CollectedTresuresvalue++;
                    }

                    anaForm.lblCollectedTresuresNumber.Text = $"Toplam hazine sayisi: {CollectedTresuresvalue}";

                    if (CollectedTresuresvalue == 16)
                    {
                        anaForm.lblCollectedLastTresure.Text = obstacleInfo;
                        anaForm.lblLastTresureStepValue.Text = $"Son hazineye ulaşıldıginda adim sayisi: {totalStepValue}";
                    }


                    tresures.Remove(tresure);
                    break;
                }
        }

        public void DiscoveredStaticObstacles(StaticObstacle obstacle)
        {
            string obstacleInfo = $"static obstacle found:{obstacle.Location.XLocation},{obstacle.Location.YLocation},{obstacle.obstacleName} ";
            if (!anaForm.lstBoxGameObjects.Items.Contains(obstacleInfo))
            {
                anaForm.lstBoxGameObjects.Items.Add(obstacleInfo);
            }

        }

        public void DiscoveredDynamicObstacles(DynamicObstacle obstacle)
        {
            string obstacleInfo = $"Dynamic obstacle found:{obstacle.Location.XLocation},{obstacle.Location.YLocation},{obstacle.obstacleName} ";
            if (!anaForm.lstBoxGameObjects.Items.Contains(obstacleInfo))
            {
                anaForm.lstBoxGameObjects.Items.Add(obstacleInfo);
            }

        }
    }
}
