#region usings
using System;
using System.ComponentModel.Composition;

using VVVV.PluginInterfaces.V1;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VColor;
using VVVV.Utils.VMath;

using VVVV.Core.Logging;
#endregion usings

namespace VVVV.Nodes
{
	#region PluginInfo
	[PluginInfo(Name = "MSD_BlendCal", Category = "Value", Help = "Basic template with one value in/out", Tags = "")]
	#endregion PluginInfo
	public class ValueMSD_BlendCalNode : IPluginEvaluate
	{
		#region fields & pins
		[Input("BlendX", DefaultValue = 1.0)]
		public ISpread<double> BlendX;
		
		[Input("BlendY", DefaultValue = 1.0)]
		public ISpread<double> BlendY;

		[Output("WidthLTRB", DefaultValue = 0.0)]
		public ISpread<double> WidthLTRB;

		#endregion fields & pins

		//called when data for any output pin is requested
		public void Evaluate(int SpreadMax)
		{
			int Xcount = BlendX.SliceCount;
			int Ycount = BlendY.SliceCount;
			int Num = 0;
			WidthLTRB.SliceCount = (Xcount-1)*(Ycount-1)*4;

			for (int i = 0; i < Ycount-1; i++){
				for (int j=0; j < Xcount-1; j++){
					for (int k=0; k<4; k++){
						switch(k)
        				{
          					case 0:
 								WidthLTRB[Num] = BlendX[j];
        						break;
        					case 1:
 								WidthLTRB[Num] = BlendY[i];
        						break;
        					case 2:
 								WidthLTRB[Num] = BlendX[j+1];
        						break;
        					case 3:
 								WidthLTRB[Num] = BlendY[i+1];
        						break;
            				default:
               					break;
        				}
						Num++;
					}
				}
			}
		}
	}
}
