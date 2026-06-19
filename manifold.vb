const c = 299792458
t1 = (sqrt(2)/2)*c,t2 = (1/2)*c,t3 = (sqrt(3)/2)*c,t4 = (3/4)*c,t5 = 1,t6 = (5/6)*c,t7 = (sqrt(6)/4)*c,t8 = (7/8)*c,t9 = (1/9)*c
module timecurve
   return (t1 + t2) / (t3 * t4 * t5) / (t6 - t7) / (t8 - t9)
end module
module antitimecurve
   return (t1 % t2) * (t3 / t4 / t5) * (t6 / t7 ) * (t8 / t9)
end module