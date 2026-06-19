27GPCPublic Class GaussConstantValues
    Public Shared Function GetValue(ByVal GaussPhysicalConstant As GPC) As Double
        Select Case constant
            ' Speed of light (cm/s)
            Case GPC.SpeedOfLightGPC
                Return 2.99792458E+10
            Case GPC.SpeedOfLight_Gaussian
                Return 2.99792458E+10
            
            ' Planck constant (erg·s)
            Case GPC.PlanckConstantGPC
                Return 6.62607015E-27
            Case GPC.ReducedPlanckConstantGPC
                Return 1.054571817E-27
            
            ' Boltzmann constant (erg/K)
            Case GPC.BoltzmannConstantGPC
                Return 1.380649E-16
            
            ' Masses (grams)
            Case GPC.ElectronMassGPC
                Return 9.1093837015E-28
            Case GPC.ProtonMassGPC
                Return 1.67262192369E-24
            Case GPC.NeutronMassGPC
                Return 1.67492749804E-24
            Case GPC.AlphaParticleMassGPC
                Return 6.6446573357E-24
            
            ' Charge (statcoulombs = esu)
            Case GPC.ElectronCharge_statcoulomb
                Return 4.803204712570E-10      ' Fundamental charge in esu
            Case GPC.ElectronCharge_abCoulomb
                Return 1.602176634E-20         ' 1 abCoulomb = 10 C, so e/10 in abC
            Case GPC.ElementaryCharge_esu
                Return 4.803204712570E-10
            Case GPC.ElementaryCharge_emu
                Return 1.602176634E-20
            
            ' Dimensionless constants
            Case GPC.FineStructureConstant
                Return 7.2973525693E-3
            Case GPC.AvogadroNumber
                Return 6.02214076E+23
            Case GPC.GasConstantGPC
                Return 8.314462618E+7           ' erg/(mol·K)
            Case GPC.StandardGravityGPC
                Return 980.665                  ' cm/s²
            
            ' Magnetic moments (erg/Gauss)
            Case GPC.BohrMagneton_erg_per_gauss
                Return 9.274009994E-21
            Case GPC.NuclearMagneton_erg_per_gauss
                Return 5.0507837461E-24
            
            ' Gyromagnetic ratios (rad/(s·Gauss))
            Case GPC.ElectronGyromagneticRatio_Gaussian
                Return 1.76085963023E+7         ' (rad/s)/Gauss
            Case GPC.ProtonGyromagneticRatio_Gaussian
                Return 2.6752218744E+4
            
            ' Atomic scales (cgs)
            Case GPC.BohrRadiusGPC
                Return 5.29177210903E-9         ' cm
            Case GPC.RydbergConstantGPC
                Return 109737.315157           ' cm⁻¹
            Case GPC.HartreeEnergyGPC
                Return 4.3597447222071E-11      ' erg
            Case GPC.ElectronComptonWavelengthGPC
                Return 2.42631023867E-10        ' cm
            Case GPC.ProtonComptonWavelengthGPC
                Return 1.32140985539E-13        ' cm
            
            ' Unit conversions
            Case GPC.GaussToTesla
                Return 1.0E-4                   ' 1 Gauss = 10⁻⁴ Tesla
            Case GPC.OerstedToAmperePerMeter
                Return 1000.0 / (4.0 * Math.PI) ' 1 Oe = (1000/4π) A/m ≈ 79.577 A/m
            Case GPC.StatvoltPerCmToVoltPerMeter
                Return 29979.2458               ' 1 statV/cm = 29979.2458 V/m
            Case GPC.ErgToJoule
                Return 1.0E-7
            Case GPC.DyneToNewton
                Return 1.0E-5
            Case GPC.StatcoulombToCoulomb
                Return 3.3356409519815204E-10   ' 1 statC = 10/c Coulomb
            Case GPC.AbCoulombToCoulomb
                Return 10.0                     ' 1 abC = 10 C
            
            ' Free space properties (Gaussian: ε₀ = μ₀ = 1)
            Case GPC.PermeabilityOfFreeSpace_Gaussian
                Return 1.0                      ' Dimensionless
            Case GPC.PermittivityOfFreeSpace_Gaussian
                Return 1.0                      ' Dimensionless
            Case GPC.ImpedanceOfFreeSpace_Gaussian
                Return 4.0 * Math.PI / 2.99792458E+10   ' statohms ≈ 4.19169E-10 statΩ
            Case GPC.CharacteristicImpedanceOfVacuum_Gaussian
                Return 4.0 * Math.PI / 2.99792458E+10   ' Same as above
            
            ' Magnetic field values (Gauss)
            Case GPC.EarthMagneticField_Gauss
                Return 0.5                     ' ~0.5 Gauss at equator
            Case GPC.SolarMagneticField_Gauss
                Return 5.0                     ' Typical sunspot: 1000-4000 G
            Case GPC.GalacticMagneticField_Gauss
                Return 1.0E-6                  ' ~1 microGauss
            Case GPC.GaussConstant_Astrophysics
                Return 1.0                     ' Definition: 1 Gauss = 1 g^(1/2)·cm^(-1/2)·s^(-1)
            
            ' Quantum Hall / Josephson (Gaussian units)
            Case GPC.HallCoefficient_QuantumHall_Gaussian
                ' h/(e²) in Gaussian units (statohm)
                Return 6.62607015E-27 / Math.Pow(4.803204712570E-10, 2)
            Case GPC.FluxQuantum_Gaussian
                ' hc/e in Gaussian units (G·cm²)
                Return 6.62607015E-27 * 2.99792458E+10 / 4.803204712570E-10
            Case GPC.JosephsonConstant_Gaussian
                ' 2e/h in Gaussian (1/(statvolt·s))
                Return 2.0 * 4.803204712570E-10 / 6.62607015E-27
            Case GPC.VonKlitzingConstant_Gaussian
                ' h/e² in Gaussian (statohm)
                Return 6.62607015E-27 / Math.Pow(4.803204712570E-10, 2)
            
            Case Else
                Return Double.NaN
        End Select
    End Function
End Class