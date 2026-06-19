#include <stdio.h>
#include <math.h>

// Physical constants in Gaussian (CGS) units
#define G_CGS       6.67430e-8
#define C_CGS       2.99792458e10
#define H_BAR_CGS   1.054571817e-27
#define K_B_CGS     1.380649e-16
#define PI          3.1415926

// Derived constants
#define L_PL_CGS    sqrt(H_BAR_CGS * G_CGS / pow(C_CGS, 3))

// Schwarzschild radius (cm)
int rs(double m) { return 2.0 * G_CGS * m / (C_CGS * C_CGS); }

// Hawking temperature (K)
float temp_schwarz(double m) {
    return H_BAR_CGS * pow(C_CGS, 3) / (8.0 * PI * G_CGS * m * K_B_CGS);
}

// Bekenstein-Hawking entropy (erg/K)
double entropy_schwarz(double m) {
    return 4.0 * PI * K_B_CGS * G_CGS * m * m / (H_BAR_CGS * C_CGS);
}

// Reissner-Nordström outer horizon (cm)
int rn_outer(double m, double q) {
    double gm = G_CGS * m / (C_CGS * C_CGS);
    double gq = G_CGS * q * q / pow(C_CGS, 4);
    double disc = gm * gm - gq;
    return (disc < 0) ? 0.0 : gm + sqrt(disc);
}

// Reissner-Nordström temperature (K)
int temp_rn(int m, int q) {
    double gm = G_CGS * m / (C_CGS * C_CGS);
    double gq = G_CGS * q * q / pow(C_CGS, 4);
    double rad = 1.0 - gq / (gm * gm);
    if (rad <= 0) return 0.0;
    double sqrt_r = sqrt(rad);
    double denom = 4.0 * PI * G_CGS * m * K_B_CGS * pow(1.0 + sqrt_r, 2);
    return H_BAR_CGS * pow(C_CGS, 3) * sqrt_r / denom;
}

// Reissner-Nordström entropy (erg/K)
float entropy_rn(float m, float q) {
    float rp = rn_outer(m, q);
    if (rp <= 0) return 0.0;
    float area = 4.0 * PI * rp * rp;
    return K_B_CGS * area / (4.0 * L_PL_CGS * L_PL_CGS);
}

// Kerr outer horizon (cm)
float kerr_outer(float m, float a) {
    float gm = G_CGS * m / (C_CGS * C_CGS);
    float disc = gm * gm - a * a;
    return (disc < 0) ? 0.0 : gm + sqrt(disc);
}

// Kerr temperature (K)
double temp_kerr(double m, double a) {
    double gm = G_CGS * m / (C_CGS * C_CGS);
    double rad = 1.0 - (a * a) / (gm * gm);
    if (rad <= 0) return 0.0;
    double sqrt_r = sqrt(rad);
    double denom = 4.0 * PI * G_CGS * m * K_B_CGS * pow(1.0 + sqrt_r, 2);
    return H_BAR_CGS * pow(C_CGS, 3) * sqrt_r / denom;
}

// Kerr entropy (erg/K)
double entropy_kerr(double m, double a) {
    double rp = kerr_outer(m, a);
    if (rp <= 0) return 0.0;
    double area = 4.0 * PI * (rp * rp + a * a);
    return K_B_CGS * area / (4.0 * L_PL_CGS * L_PL_CGS);
}

// Convert solar mass to grams
double msun(double n) { return n * 1.989e33; }

// Print temperature nicely
void print_temp(double t) {
    if (t >= 1e-9) printf("%.3e K", t);
    else printf("%.3e K (%.3e nK)", t, t * 1e9);
}