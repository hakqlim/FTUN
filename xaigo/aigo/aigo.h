#pragma once
// aigo.h

//#ifndef __AFXWIN_H__

//#error  "PCH에 대해 이 파일을 포함하기 전에 'stdafx.h'를 포함합니다."

//#endif

#ifndef __AIGO__

#define __AIGO__

int WINAPI BiN(int m, double s);
int WINAPI Geom(double m);
int WINAPI Poiss(double m);

double WINAPI Expon(double m);
double WINAPI Gamma(double m, double s);
double WINAPI LogN(double m, double s);
double WINAPI Norma(double m, double s);
double WINAPI UniFoR(double m, double s);
double WINAPI BetaR(double m, double s);
int WINAPI APlusB(int nA, int nB);

#endif