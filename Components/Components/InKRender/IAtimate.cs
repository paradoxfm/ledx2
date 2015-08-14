using System;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace LEDX.Components {

	public interface IAtimate {

		bool? IsPlay { get; set; }

		double PlayTime { get; set; }

		Model.Controller Contr { get; set; }

		void UpdateAnimation();

		LinearColorKeyFrame GetFrame(Color cl, double time);
	}
}
