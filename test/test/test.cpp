// test.cpp : DLL 응용 프로그램을 위해 내보낸 함수를 정의합니다.
//

#include "stdafx.h"
#include <iostream>

#include <random>
#include <cstdlib>
#include <chrono>
#include <sstream>
#include <string>

/* functions */
#include "test.h" // use here the name of your header file

/*========================================================
 AddLongs_Pointer - a function to add all elements of an array passed
 as a pointer (int *)
 returns the sum of the elements
==========================================================*/

void __declspec (dllexport) __stdcall AddLongs_Pointer(double *plArrayOfLongs, int lElements, int Itype, double m, double s)
{
	int i;

	std::default_random_engine generator;
	std::random_device rd;

	std::cout << lElements << "  IT=" << Itype << "  m=" << m << "  s=" << s << std::endl;

	if (Itype == 1)
	{
			std::lognormal_distribution<double> distribution1(m, s);
			for (i = 0; i < lElements; i++)
			{
				generator.seed(rd());
				plArrayOfLongs[i] = distribution1(generator);
			}
		}
	else if (Itype == 3)
	{
		std::uniform_real_distribution<double> distribution3(m, s);
			for (i = 0; i < lElements; i++)
			{
				generator.seed(rd());
				plArrayOfLongs[i] = distribution3(generator);
			}
		}
	else if (Itype == 4)
		{
			std::gamma_distribution<double> distribution4(m, s);
			for (i = 0; i < lElements; i++)
			{
				generator.seed(rd());
				plArrayOfLongs[i] = distribution4(generator);
			}
		}
	else if (Itype == 5) //Beta distribution for HFE
	{
		for (i = 0; i < lElements; i++)
		{
			std::default_random_engine generator;
			std::random_device rd;

			generator.seed(rd());

			std::gamma_distribution<double> distributionX(m, 1.0);
			double Xnumber = distributionX(generator);

			std::gamma_distribution<double> distributionY(s, 1.0);
			double Ynumber = distributionY(generator);

			double number = Xnumber / (Xnumber + Ynumber);
			plArrayOfLongs[i] = number;
		}
	}
	std::cout << plArrayOfLongs[10] << std::endl;
	return;
}

//[출처] DLL에 VB배열 넘겨주기 | 작성자 aragagi


