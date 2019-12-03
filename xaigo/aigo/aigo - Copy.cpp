// aigo.cpp : DLL 응용 프로그램을 위해 내보낸 함수를 정의합니다.
//

#include "stdafx.h"
#include <iostream>
#include <random>
#include <cstdlib>
#include <chrono>

// aigo.cpp

#include "aigo.h"

int WINAPI BiN(int m, double s)
{
	std::default_random_engine generator;
	std::binomial_distribution<int> distribution(m, s);
	std::random_device rd;
	
	generator.seed(rd());

	double number = distribution(generator);
	std::cout << number << std::endl;
	return number;
}

int WINAPI Geom(double m)
{
	std::default_random_engine generator;
	std::geometric_distribution<int> distribution(m);
	std::random_device rd;

	generator.seed(rd());

	double number = distribution(generator);
	std::cout << number << std::endl;
	return number;
}

int WINAPI Poiss(double m)
{
	std::default_random_engine generator;
	std::poisson_distribution<int> distribution(m);
	std::random_device rd;

	generator.seed(rd());

	double number = distribution(generator);
	std::cout << number << std::endl;
	return number;
}

double WINAPI Expon(double m)
{
	std::default_random_engine generator;
	std::exponential_distribution<double> distribution(m);
	std::random_device rd;

	generator.seed(rd());

	double number = distribution(generator);
	std::cout << number << std::endl;
	return number;
}

double WINAPI Gamma(double m, double s)
{
	std::default_random_engine generator;
	std::gamma_distribution<double> distribution(m, s);
	std::random_device rd;

	generator.seed(rd());

	double number = distribution(generator);
	std::cout << number << std::endl;
	return number;
}

double WINAPI Norma(double m, double s)
{
	std::default_random_engine generator;
	std::normal_distribution<double> distribution(m,s);
	std::random_device rd;

	generator.seed(rd());

	double number = distribution(generator);
	std::cout << number << std::endl;
	return number;
}

double WINAPI LogN(double m, double s) 
{
	std::default_random_engine generator;
	std::lognormal_distribution<double> distribution(m,s);
	std::random_device rd;

	//typedef std::chrono::high_resolution_clock myclock;
	//myclock::time_point beginning = myclock::now();
	//// obtain a seed from the timer
	//myclock::duration d = myclock::now() - beginning;
	//unsigned seed2 = d.count();

	generator.seed(rd());

	double number = distribution(generator);
	std::cout << number << std::endl;
	return number;
}

double WINAPI UniFoR(double m, double s)
{
	std::default_random_engine generator;
	std::uniform_real_distribution<double> distribution(m, s);
	std::random_device rd;

	generator.seed(rd());

	double number = distribution(generator);
	std::cout << number << std::endl;
	return number;
}

int WINAPI APlusB(int nA, int nB)
{
	return nA + nB;
}

