using ClusterSkimmer;
using FluentAssertions;
using System;
using Xunit;

namespace ClusterSkimmerTests
{
    public class RealClusterSpotTests
    {
        [Fact]
        public void Test_Blank()
        {
            ClusterSpot.TryParse("", out _).Should().BeFalse();
        }

        [Fact]
        public void Test_Prompt()
        {
            ClusterSpot.TryParse("M0LTE de VE7CC-1 05-Jan-2021 1333Z   CCC >", out _).Should().BeFalse();
        }

        [Fact]
        public void Test_011ead91_ddf3_4092_b05c_d9da29e43f13()
        {
            ClusterSpot.TryParse("DX de SJ2W-#:    14060.0  IK2JET       CW  4 dB 21 WPM CQ             1048Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "SJ2W", null, 14060000, "IK2JET", "CW", 4, 21, "CQ", 10, 48);
        }

        [Fact]
        public void Test_20724ee2_2ee6_4c5f_9c13_efb4515c24cd()
        {
            ClusterSpot.TryParse("DX de WZ7I-#:    18110.0  CS3B         CW 19 dB 23 WPM NCDXF BCN      1122Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "WZ7I", null, 18110000, "CS3B", "CW", 19, 23, "NCDXF BCN", 11, 22);
        }

        [Fact]
        public void Test_52c46ca3_23db_475b_ab42_d1d13bc937df()
        {
            ClusterSpot.TryParse("DX de SQ5AM:      7170.5  E79Q                                        1117Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "SQ5AM", null, 7170500, "E79Q", null, null, null, null, 11, 17);
        }

        [Fact]
        public void Test_b8d0b826_8539_41f8_858c_d7f30c7d38a7()
        {
            ClusterSpot.TryParse("DX de EA8/DF4UE-#:21149.0  IT9ATQ/B    CW  9 dB 10 WPM BEACON         1118Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "EA8/DF4UE", null, 21149000, "IT9ATQ/B", "CW", 9, 10, "BEACON", 11, 18);
        }

        [Fact]
        public void Test_b8d0b826_8539_41f8_858c_d7f30c7d38a7_ssid()
        {
            ClusterSpot.TryParse("DX de EA8/DF4UE-12:21149.0  IT9ATQ/B    CW  9 dB 10 WPM BEACON         1118Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "EA8/DF4UE", 12, 21149000, "IT9ATQ/B", "CW", 9, 10, "BEACON", 11, 18);
        }

        [Fact]
        public void Test_6e243d45_5955_48a4_a7e9_63784fb80f03()
        {
            ClusterSpot.TryParse("DX de DL9GTB-#:  14071.3  RY21NY       PSK63 42 dB CQ                 1118Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "DL9GTB", null, 14071300, "RY21NY", "PSK63", 42, null, "CQ", 11, 18);
        }

        [Fact]
        public void Test_b8352d0e_adc7_404d_a432_b4cb14b495b1()
        {
            ClusterSpot.TryParse("DX de SM6FMB-#:  14084.0  SV3/SV1NN    RTTY +10 dB CQ                 1118Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "SM6FMB", null, 14084000, "SV3/SV1NN", "RTTY", 10, null, "CQ", 11, 18);
        }

        [Fact]
        public void Test_bf798ebc_89a1_4e7c_91cb_aec6f398dbbf()
        {
            ClusterSpot.TryParse("DX de G4ZFE-#:   14086.6  RA4L         RTTY + 8 dB CQ                 1118Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "G4ZFE", null, 14086600, "RA4L", "RTTY", 8, null, "CQ", 11, 18);
        }

        [Fact]
        public void Test_bf798ebc_89a1_4e7c_91cb_aec6f398dbbf_neg()
        {
            ClusterSpot.TryParse("DX de G4ZFE-#:   14086.6  RA4L         RTTY - 8 dB CQ                 1118Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "G4ZFE", null, 14086600, "RA4L", "RTTY", -8, null, "CQ", 11, 18);
        }

        [Fact]
        public void Test_34483244_1b8e_4e3c_aaa7_2798edd72c8c()
        {
            ClusterSpot.TryParse("DX de IZ1HHT:    21229.0  RA3VLA       TnX Nickolay 73                1118Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "IZ1HHT", null, 21229000, "RA3VLA", null, null, null, "TnX Nickolay 73", 11, 18);
        }

        [Fact]
        public void Test_5deb28d9_c17c_4c25_9e86_e67c17f2f602()
        {
            ClusterSpot.TryParse("DX de G4ZFE-#:   14070.4  SP7OGQ       PSK31 24 dB CQ                 1118Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "G4ZFE", null, 14070400, "SP7OGQ", "PSK31", 24, null, "CQ", 11, 18);
        }

        [Fact]
        public void Test_438a0729_7891_4e87_9901_4420c8734669()
        {
            ClusterSpot.TryParse("DX de VU2CPL-#:  14014.9  F5GPE        CW  4 dB 20 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "VU2CPL", null, 14014900, "F5GPE", "CW", 4, 20, "CQ", 13, 33);
        }

        [Fact]
        public void Test_83674357_f770_4fe3_83a2_a602ec6c3b75()
        {
            ClusterSpot.TryParse("DX de RN4WA-#:    7025.0  YU5D         CW 14 dB 27 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "RN4WA", null, 7025000, "YU5D", "CW", 14, 27, "CQ", 13, 33);
        }

        [Fact]
        public void Test_e40870c2_5d0d_4373_a29f_5b0843bd37a7()
        {
            ClusterSpot.TryParse("DX de W2AXR-#:   14052.0  K3Y/5        CW 25 dB 15 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "W2AXR", null, 14052000, "K3Y/5", "CW", 25, 15, "CQ", 13, 33);
        }

        [Fact]
        public void Test_b3980d60_e8d3_4334_830f_a05e1192ebc7()
        {
            ClusterSpot.TryParse("DX de OE6ADD-#:   7021.0  LZ1VKD       CW 19 dB 23 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "OE6ADD", null, 7021000, "LZ1VKD", "CW", 19, 23, "CQ", 13, 33);
        }

        [Fact]
        public void Test_b3053817_a771_4f71_96db_162c067aa80b()
        {
            ClusterSpot.TryParse("DX de WE9V-#:    14008.0  EW7LO        CW 11 dB 24 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "WE9V", null, 14008000, "EW7LO", "CW", 11, 24, "CQ", 13, 33);
        }

        [Fact]
        public void Test_496d254d_dbca_4d0e_b8c6_8a64b2ef3e46()
        {
            ClusterSpot.TryParse("DX de EA1URA-#:   7015.6  RM7C         CW  6 dB 31 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "EA1URA", null, 7015600, "RM7C", "CW", 6, 31, "CQ", 13, 33);
        }

        [Fact]
        public void Test_102c6f9b_4bfc_4abf_81e0_df96496dd669()
        {
            ClusterSpot.TryParse("DX de WZ7I-#:    14018.0  K3RA         CW 19 dB 29 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "WZ7I", null, 14018000, "K3RA", "CW", 19, 29, "CQ", 13, 33);
        }

        [Fact]
        public void Test_9a5b5925_2e6c_408d_bad6_1b41789e2f74()
        {
            ClusterSpot.TryParse("DX de ES3V-#:     7030.5  DK4YF        CW  7 dB 21 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "ES3V", null, 7030500, "DK4YF", "CW", 7, 21, "CQ", 13, 33);
        }

        [Fact]
        public void Test_66e6ae4b_7283_4f13_b143_08d70a0c5c98()
        {
            ClusterSpot.TryParse("DX de CT7ANO-#:  14009.0  SM1HEV       CW  7 dB 24 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "CT7ANO", null, 14009000, "SM1HEV", "CW", 7, 24, "CQ", 13, 33);
        }

        [Fact]
        public void Test_c63d1b98_8d3f_458f_9bac_b5e10370e3e8()
        {
            ClusterSpot.TryParse("DX de W3LPL-#:   18087.0  DL8NBM       CW  5 dB 20 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "W3LPL", null, 18087000, "DL8NBM", "CW", 5, 20, "CQ", 13, 33);
        }

        [Fact]
        public void Test_04355dce_faf1_4f98_9b80_3ce4716fae77()
        {
            ClusterSpot.TryParse("DX de W3LPL-#:   14041.0  SA6AUT       CW 13 dB 19 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "W3LPL", null, 14041000, "SA6AUT", "CW", 13, 19, "CQ", 13, 33);
        }

        [Fact]
        public void Test_e7ee354d_d1ce_4001_8ac8_2ecf916d56c6()
        {
            ClusterSpot.TryParse("DX de SE5E-#:     7037.6  IZ2FDU       CW 20 dB 25 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "SE5E", null, 7037600, "IZ2FDU", "CW", 20, 25, "CQ", 13, 33);
        }

        [Fact]
        public void Test_51c89c5d_b7b8_4faf_91c5_83c064f64ff0()
        {
            ClusterSpot.TryParse("DX de G0LUJ-#:   10116.0  YO4NF        CW  3 dB 32 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "G0LUJ", null, 10116000, "YO4NF", "CW", 3, 32, "CQ", 13, 33);
        }

        [Fact]
        public void Test_6f31abef_8f21_4a59_a839_f20948b6a4ca()
        {
            ClusterSpot.TryParse("DX de HS8KVH-#:   7011.0  RN3CT        CW  9 dB 20 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "HS8KVH", null, 7011000, "RN3CT", "CW", 9, 20, "CQ", 13, 33);
        }

        [Fact]
        public void Test_46a03759_c165_425a_8147_e58018b0355f()
        {
            ClusterSpot.TryParse("DX de TF4X-#:    14018.9  LB6GG        CW 33 dB 25 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "TF4X", null, 14018900, "LB6GG", "CW", 33, 25, "CQ", 13, 33);
        }

        [Fact]
        public void Test_fb45541e_d6a9_4bd8_a044_434f50b126b2()
        {
            ClusterSpot.TryParse("DX de KM3T-2-#:  14022.5  LZ1FH        CW 13 dB 25 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "KM3T", 2, 14022500, "LZ1FH", "CW", 13, 25, "CQ", 13, 33);
        }

        [Fact]
        public void Test_de2a2056_9624_4823_bb37_bfc6456dd94f()
        {
            ClusterSpot.TryParse("DX de W9XG-#:     7030.5  W8KJP        CW  9 dB 21 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "W9XG", null, 7030500, "W8KJP", "CW", 9, 21, "CQ", 13, 33);
        }

        [Fact]
        public void Test_a2c5e8fd_3b22_47ef_81b7_fa941e407703()
        {
            ClusterSpot.TryParse("DX de K1TTT-#:   10107.0  SB7S         CW  5 dB 22 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "K1TTT", null, 10107000, "SB7S", "CW", 5, 22, "CQ", 13, 33);
        }

        [Fact]
        public void Test_6765f140_904c_4ff6_8d57_e1805bff0c40()
        {
            ClusterSpot.TryParse("DX de K3LR-#:     3554.0  K3Y/9        CW 13 dB 13 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "K3LR", null, 3554000, "K3Y/9", "CW", 13, 13, "CQ", 13, 33);
        }

        [Fact]
        public void Test_cba045ad_3476_478a_b98d_c1139dd84f44()
        {
            ClusterSpot.TryParse("DX de DK9IP-#:    7013.0  RL21NY       CW 19 dB 35 WPM CQ             1333Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "DK9IP", null, 7013000, "RL21NY", "CW", 19, 35, "CQ", 13, 33);
        }

        [Fact]
        public void Test_b01c7877_a5cd_4695_bb61_70b46b6441c2()
        {
            ClusterSpot.TryParse("DX de IK4VET-1-#: 7039.0  DJ6UX        CW 25 dB 24 WPM CQ             1334Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_9f7ab90e_3d99_4b5b_adf8_444175b9d841()
        {
            ClusterSpot.TryParse("DX de W8WTS-#:   21053.0  F6HKA        CW 10 dB 29 WPM CQ             1334Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_d776706f_d085_407c_bc3d_849b1248061b()
        {
            ClusterSpot.TryParse("DX de N5RZ-#:    14028.1  CM6SQ        CW 10 dB 24 WPM CQ             1334Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_467ad7e8_47f7_4281_b3ae_aa8c46024dae()
        {
            ClusterSpot.TryParse("DX de W3UA-#:    14024.0  RN5AA        CW 20 dB 27 WPM CQ             1334Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_9d325b88_4f24_4a0d_9275_09cce7ae0dd2()
        {
            ClusterSpot.TryParse("DX de SE5E-#:     3540.0  UA1ALY       CW 10 dB 18 WPM CQ             1334Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_7e23ee6e_723f_48a4_bca9_32d53972385c()
        {
            ClusterSpot.TryParse("DX de DL9GTB-#:  14007.1  DP4E         CW  8 dB 27 WPM CQ             1334Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_6960b6cf_d13d_4a99_b113_a8e7740aeb1b()
        {
            ClusterSpot.TryParse("DX de WS3W-#:     7046.0  WB8BIL       CW 21 dB 15 WPM CQ             1334Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_84eac475_2db6_45b3_8243_e5a01ff28135()
        {
            ClusterSpot.TryParse("DX de HB9DCO-#:  10115.3  YU1U         CW 14 dB 24 WPM CQ             1334Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_cedd7b09_f103_4334_9b89_f27de6df1f7f()
        {
            ClusterSpot.TryParse("DX de VE6WZ-#:    1821.5  AA6AA        CW 37 dB 22 WPM CQ             1334Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_62db0ca6_ad13_4ae3_9d5e_71b0065f1e1a()
        {
            ClusterSpot.TryParse("DX de DK3UA-#:    7035.1  EA2DPA/P     CW  5 dB 28 WPM CQ             1334Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_e70f00d3_6773_40f8_9880_33d9a08d52a1()
        {
            ClusterSpot.TryParse("DX de 4Z5ML:     14018.0  K3RA         599 CQ                         1334Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_e228f6e3_c9eb_4a79_8997_e17e86e3d65e()
        {
            ClusterSpot.TryParse("DX de S59AT:     14207.0  KE5EE        cq dx                          1334Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "S59AT", null, 14207000, "KE5EE", null, null, null, "cq dx", 13, 34);
        }

        [Fact]
        public void Test_3a5047b3_d6be_4ac3_9522_5b5d28fb1592()
        {
            ClusterSpot.TryParse("DX de OH6BG-#:   14045.9  F6CRP        CW 19 dB 20 WPM CQ             1334Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_b5a7641d_4135_465c_bc6e_e4b544a1687e()
        {
            ClusterSpot.TryParse("DX de 3V/KF5EYY-#:18070.0  G4SPY       CW 20 dB 27 WPM CQ             1334Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_9d854f30_0668_4e4c_9c1d_d37ba9af82bc()
        {
            ClusterSpot.TryParse("DX de SM7IUN-#:  14016.0  II4BLD       CW 18 dB 30 WPM CQ             1334Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_ca6072f0_07f8_4659_bfd8_5a5781fbdad6()
        {
            ClusterSpot.TryParse("DX de K1TTT-#:   14013.0  OV1CDX       CW 18 dB 21 WPM CQ             1334Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_8affd487_33d1_4ac1_a52b_2b7053c6ed39()
        {
            ClusterSpot.TryParse("DX de S50U-#:     7022.9  IR0MMI       CW 15 dB 25 WPM CQ             1334Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_4a45f651_91bf_40ba_930c_3cd4bc985468()
        {
            ClusterSpot.TryParse("DX de N5RZ-#:     7024.5  K1NVY        CW 33 dB 27 WPM CQ             1334Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_038ef7f7_e1b8_4437_a9a8_d53ab44269d6()
        {
            ClusterSpot.TryParse("DX de SE5E-#:     7021.0  II2MMI       CW 23 dB 24 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_329da799_504a_472f_8905_94f96ecc4c3b()
        {
            ClusterSpot.TryParse("DX de SE5E-#:    10115.0  OP19MSF      CW 31 dB 28 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_61516e76_b4fa_4713_ac0c_f793b7b6af8a()
        {
            ClusterSpot.TryParse("DX de K3PA-#:    14009.0  SM1HEV       CW 10 dB 24 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_955b9385_59c7_496c_98d6_a6cf97c02f29()
        {
            ClusterSpot.TryParse("DX de DF7GB-#:    7025.0  YU5D         CW 20 dB 27 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_0898bb40_5f21_4592_9041_bc04d6e9b33e()
        {
            ClusterSpot.TryParse("DX de SM6FMB-#:   3539.0  SP5LXT       CW  9 dB 16 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_754872de_c418_4917_ad48_c2766b53e3dd()
        {
            ClusterSpot.TryParse("DX de KM3T-2-#:  14019.0  LB6GG        CW 20 dB 25 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_fff3f37f_8b82_4784_9c7c_493afe6b92c6()
        {
            ClusterSpot.TryParse("DX de W3LPL-#:   14035.0  DH1GAP       CW 19 dB 18 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_79819ffc_d94a_40ad_b5ca_8ab53207c9b0()
        {
            ClusterSpot.TryParse("DX de DR4W-#:    14016.0  II4BLD       CW 20 dB 29 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_7f796e4a_86f2_43e4_9e8f_85b0a4ef8f01()
        {
            ClusterSpot.TryParse("DX de DL9GTB-#:  18076.5  WH6LE        CW  9 dB 20 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_4ebe8b1d_c78a_4a35_b058_7b1d4dfa8e93()
        {
            ClusterSpot.TryParse("DX de K3LR-#:    14028.1  CM6SQ        CW 26 dB 31 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_1583438c_96ef_449c_8f83_e4ebbd7bbf09()
        {
            ClusterSpot.TryParse("DX de UA4M-#:    10117.1  DM2RN        CW  3 dB 28 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_dc57ee4a_78f4_470f_94c1_042cc9d5a63f()
        {
            ClusterSpot.TryParse("DX de W2AXR-#:    7041.0  KR0M         CW 25 dB 20 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_91d514c3_6146_4cc5_bad9_40bd4a1fc517()
        {
            ClusterSpot.TryParse("DX de W3RGA-#:   14004.6  K9PPY        CW 19 dB 22 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_aef7ff9f_488f_4701_9cbc_8809c82b0998()
        {
            ClusterSpot.TryParse("DX de SM7IUN-#:   3530.0  RC21NY       CW 22 dB 32 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_6816a2f1_9c61_4a71_815c_69cb16534442()
        {
            ClusterSpot.TryParse("DX de EA5WU-#:   14005.6  OK7MD        CW 14 dB 22 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_a583a823_8deb_4844_be75_0f23145fe472()
        {
            ClusterSpot.TryParse("DX de WZ7I-#:    18079.0  OK2PVF       CW 31 dB 25 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_5663e3d1_b7f3_410c_9b24_f279a3df35f0()
        {
            ClusterSpot.TryParse("DX de WZ7I-#:     7054.0  N2DGQ        CW 28 dB 21 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_e99e6fc1_604d_42f9_9348_307e80f2eeb1()
        {
            ClusterSpot.TryParse("DX de PJ2A-#:    14041.0  SA6AUT       CW  4 dB 19 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_c04cd814_0647_44f8_8035_50bde7bfba95()
        {
            ClusterSpot.TryParse("DX de UA4M-#:    10103.5  ON4EH        CW 25 dB 31 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_f8f21389_0a69_486a_9889_9bcda60e77d6()
        {
            ClusterSpot.TryParse("DX de UA4M-#:    10113.0  IU1MRY       CW 10 dB 23 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_aef68ba4_3e47_4663_81ee_96ade15277fa()
        {
            ClusterSpot.TryParse("DX de K3LR-#:    14010.0  YO4BUA       CW 20 dB 22 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_05b54f50_e936_4ee8_8079_1933568d7281()
        {
            ClusterSpot.TryParse("DX de SM7IUN-#:   7027.0  RU1QD        CW 22 dB 31 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_b03e75de_2ab3_4d05_b611_029a8bf42dbe()
        {
            ClusterSpot.TryParse("DX de EA5WU-#:   14021.1  UT0IW        CW  7 dB 24 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_cb0e7733_9314_4883_8e96_9aea6f3fa635()
        {
            ClusterSpot.TryParse("DX de ON6ZQ-#:   14033.1  UR5ZQV       CW  3 dB 25 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_0d4b0793_be77_42de_aa99_ec68086bb7cf()
        {
            ClusterSpot.TryParse("DX de G0LUJ-#:   10105.0  LY2PX        CW  9 dB 27 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_f649fa83_968c_4630_a637_b830a6c5f7c5()
        {
            ClusterSpot.TryParse("DX de TF4X-#:    10118.0  DL1LBV       CW 21 dB 24 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_dab40dd7_d4fc_44bb_b70d_f3652fe0f8e5()
        {
            ClusterSpot.TryParse("DX de DK3UA-#:   10110.1  SX7A         CW 11 dB 21 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_96003500_dcd6_44f4_b296_57f55c8bbe98()
        {
            ClusterSpot.TryParse("DX de 3D2AG-#:    7014.1  VK3QB        CW 10 dB 20 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_6524f269_f32a_4b9f_b0e5_f87e1b4605ed()
        {
            ClusterSpot.TryParse("DX de SM6FMB-#:   3507.0  UR9QW        CW  2 dB 20 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_f84bd278_4190_4622_9a94_5eb061319105()
        {
            ClusterSpot.TryParse("DX de W3OA-2-#:  10129.4  W0ERE/B      CW  6 dB 17 WPM BEACON         1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_6491ec23_c1f8_464c_a56e_f620a3cf2fa7()
        {
            ClusterSpot.TryParse("DX de SZ1A-#:     7030.4  R8TA         CW  4 dB 22 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_40002e67_b649_4e19_a358_9a6d9319fbea()
        {
            ClusterSpot.TryParse("DX de DO4DXA-#:   7018.0  RC21NY       CW  4 dB 23 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_cf24db04_56a1_4e95_89ca_b175fd6d7133()
        {
            ClusterSpot.TryParse("DX de W8WTS-#:    7051.0  K3Y/6        CW 21 dB 13 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_680663e2_fb1b_4fec_a2f0_a25969ae384f()
        {
            ClusterSpot.TryParse("DX de TF3Y-#:    18089.0  YT1T         CW  3 dB 32 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_2b2be9fa_f5f6_40ed_b3d8_056ccb114921()
        {
            ClusterSpot.TryParse("DX de DE1LON-#:   7030.5  OK1IF        CW 19 dB 22 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_2ad60012_0ff6_470c_8edd_febf0d79f78b()
        {
            ClusterSpot.TryParse("DX de VK4CT-#:    3501.0  JD1BMH       CW 28 dB 25 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_3e3553af_e099_4d85_b998_4d926f13bdaa()
        {
            ClusterSpot.TryParse("DX de DK3UA-#:   10112.9  R4ACY        CW  4 dB 24 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_fea2b37e_af7f_46c1_b6ac_8130925bdbef()
        {
            ClusterSpot.TryParse("DX de PA3AIN-#:  10115.7  YU1U         CW 25 dB 26 WPM CQ             1346Z", out var spot).Should().BeTrue();
        }

        [Fact]
        public void Test_18e95a13_9be1_4704_afb6_360b53e84c85()
        {
            ClusterSpot.TryParse("DX de N3NBT:     14264.0  OG60IPA                                     1348Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "N3NBT", null, 14264000, "OG60IPA", null, null, null, null, 13, 48);
        }

        [Fact]
        public void Test_a9d577ee_9143_4888_b8c2_55e5cb0705e1()
        {
            ClusterSpot.TryParse("DX de DL9GTB-#:  14071.5  EA7FDO       PSK31 20 dB CQ                 1349Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "DL9GTB", null, 14071500, "EA7FDO", "PSK31", 20, null, "CQ", 13, 49);
        }

        [Fact]
        public void Test_43ffd528_0fba_45ba_92b1_d9a1347c6c96()
        {
            ClusterSpot.TryParse("DX de KC1BBU:    18125.0  G0IUA        tnx fer contact STUART         1349Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "KC1BBU", null, 18125000, "G0IUA", null, null, null, "tnx fer contact STUART", 13, 49);
        }

        [Fact]
        public void Test_e729f082_5f6f_4558_b253_48c17be973fb()
        {
            ClusterSpot.TryParse("DX de DL9GTB-#:  14071.7  TA4Q         PSK31 16 dB CQ                 1349Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "DL9GTB", null, 14071700, "TA4Q", "PSK31", 16, null, "CQ", 13, 49);
        }

        [Fact]
        public void Test_f9fd5abd_8190_43eb_9938_5ac465da15f8()
        {
            ClusterSpot.TryParse("DX de HB9CAT-#:   7041.1  UW3HM        RTTY +16 dB CQ                 1349Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "HB9CAT", null, 7041100, "UW3HM", "RTTY", 16, null, "CQ", 13, 49);
        }

        [Fact]
        public void Test_24669f47_6d79_4758_8e08_f9296e2cfdee()
        {
            ClusterSpot.TryParse("DX de SV1AHH:    10110.0  SX7A         SES                            1349Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "SV1AHH", null, 10110000, "SX7A", null, null, null, "SES", 13, 49);
        }

        [Fact]
        public void Test_0213eba5_8543_4e93_9c7b_00908c005d56()
        {
            ClusterSpot.TryParse("DX de DL1NDY:    14249.0  SV9IOI       KRETA                          1350Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "DL1NDY", null, 14249000, "SV9IOI", null, null, null, "KRETA", 13, 50);
        }

        [Fact]
        public void Test_a0a69b50_5bd5_430c_8c33_3b15e8e345a3()
        {
            ClusterSpot.TryParse("DX de CN8AMA:    14207.0  KE5EE        TNX QSO 59                     1350Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "CN8AMA", null, 14207000, "KE5EE", null, null, null, "TNX QSO 59", 13, 50);
        }

        [Fact]
        public void Test_f7498cb1_d9c7_4dfa_9967_7315dc92d86b()
        {
            ClusterSpot.TryParse("DX de DL9GTB-#:  14072.0  R6FS         PSK31 30 dB CQ                 1350Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "DL9GTB", null, 14072000, "R6FS", "PSK31", 30, null, "CQ", 13, 50);
        }

        [Fact]
        public void Test_ba4381fa_c0e2_45fd_bc2e_e425329c6b14()
        {
            ClusterSpot.TryParse("DX de N8AE:      14243.0  SQ9ORQ                                      1350Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "N8AE", null, 14243000, "SQ9ORQ", null, null, null, null, 13, 50);
        }

        [Fact]
        public void Test_f10c9b0b_6011_4461_8490_93ccac101039()
        {
            ClusterSpot.TryParse("DX de VK4HG:     14195.0  S79VU                                       1350Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "VK4HG", null, 14195000, "S79VU", null, null, null, null, 13, 50);
        }

        [Fact]
        public void Test_0b4ff8de_9966_469f_a167_32f15508f12c()
        {
            ClusterSpot.TryParse("DX de ON4KWT:    14207.0  KE5EE        TNX for the QSO Claude         1351Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "ON4KWT", null, 14207000, "KE5EE", null, null, null, "TNX for the QSO Claude", 13, 51);
        }

        [Fact]
        public void Test_511856b1_acfa_465c_9990_19851d67f9cf()
        {
            ClusterSpot.TryParse("DX de YD0AUU:     7160.0  YF2BMJ       CQ DX with co                  1358Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "YD0AUU", null, 7160000, "YF2BMJ", null, null, null, "CQ DX with co", 13, 58);
        }

        [Fact]
        public void Test_78850b5e_031b_442c_ab1d_0698e04e87fb()
        {
            ClusterSpot.TryParse("DX de DH8WG-10:   7048.0  IU1DEI       Pietro TNX HELL QSO 73         1358Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "DH8WG", 10, 7048000, "IU1DEI", null, null, null, "Pietro TNX HELL QSO 73", 13, 58);
        }

        [Fact]
        public void Test_144b5a4f_4f5e_4ac6_ab25_3f977fb0764c()
        {
            ClusterSpot.TryParse("DX de DL9GTB-#:  14071.7  TA4Q         PSK31 25 dB CQ                 1358Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "DL9GTB", null, 14071700, "TA4Q", "PSK31", 25, null, "CQ", 13, 58);
        }

        [Fact]
        public void Test_b38f9bf0_f28f_4242_aafb_1ab635f35766()
        {
            ClusterSpot.TryParse("DX de W8DEO:     14243.0  SQ9ORQ       5-9 Ohio                       1359Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "W8DEO", null, 14243000, "SQ9ORQ", null, null, null, "5-9 Ohio", 13, 59);
        }

        [Fact]
        public void Test_93158918_144d_4b0d_a6cf_0f26950043fd()
        {
            ClusterSpot.TryParse("DX de ON4KWT:    14249.0  SV9IOI       TNX for the QSO Claude         1359Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "ON4KWT", null, 14249000, "SV9IOI", null, null, null, "TNX for the QSO Claude", 13, 59);
        }

        [Fact]
        public void Test_32b720b7_c448_45fb_b087_387b563af9ad()
        {
            ClusterSpot.TryParse("DX de DM1AA:      7005.1  4U1A                                        1400Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "DM1AA", null, 7005100, "4U1A", null, null, null, null, 14, 00);
        }

        [Fact]
        public void Test_a79d4e13_bc39_43cf_be7a_d9e272672fa6()
        {
            ClusterSpot.TryParse("DX de G4ZFE-#:    7041.4  RA3DFQ       PSK31 19 dB CQ                 1400Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "G4ZFE", null, 7041400, "RA3DFQ", "PSK31", 19, null, "CQ", 14, 00);
        }

        [Fact]
        public void Test_07f4e4e7_ed26_4642_9a3e_b27dd34bfeb9()
        {
            ClusterSpot.TryParse("DX de DL9GTB-#:  14070.9  EA3CS        PSK31 42 dB CQ                 1400Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "DL9GTB", null, 14070900, "EA3CS", "PSK31", 42, null, "CQ", 14, 00);
        }

        [Fact]
        public void Test_381eb99f_5ca5_42e0_996a_031b6c237ca3()
        {
            ClusterSpot.TryParse("DX de DL9GTB-#:   7041.5  RA3DFQ       PSK31 23 dB CQ                 1400Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "DL9GTB", null, 7041500, "RA3DFQ", "PSK31", 23, null, "CQ", 14, 00);
        }

        [Fact]
        public void Test_189bae0e_1143_4067_a42d_0084bd5d9403()
        {
            ClusterSpot.TryParse("DX de N7EKD:      7167.0  YC9VED       CQ  CQ DX  PAPUA NEW GUINA     1400Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "N7EKD", null, 7167000, "YC9VED", null, null, null, "CQ  CQ DX  PAPUA NEW GUINA", 14, 00);
        }

        [Fact]
        public void Test_bc672741_6225_4117_b053_8de02ec3558c()
        {
            ClusterSpot.TryParse("DX de OE9GHV-#:   7034.9  HA0HI        RTTY +35 dB CQ                 1400Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "OE9GHV", null, 7034900, "HA0HI", "RTTY", 35, null, "CQ", 14, 00);
        }

        [Fact]
        public void Test_0f293aaf_b015_4b45_afee_84323f70d558()
        {
            ClusterSpot.TryParse("DX de LY1NM:     10117.7  YU1TY        cq  cq                         1400Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "LY1NM", null, 10117700, "YU1TY", null, null, null, "cq  cq", 14, 00);
        }

        [Fact]
        public void Test_5a0b3e79_f28d_4b50_a81a_cb9e7bb72658()
        {
            ClusterSpot.TryParse("DX de LU5EPB-#:  14074.0  W0RIC        FT8  -16 dB  928 Hz            1442Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "LU5EPB", null, 14074000, "W0RIC", "FT8", -16, null, null, 14, 42, hzOffset: 928);
        }

        [Fact]
        public void Test_ed013a75_7475_49e9_b07c_39355e973766()
        {
            ClusterSpot.TryParse("DX de NC7J-#:    14074.0  WB9HLA       FT8  - 5 dB                    1443Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "NC7J", null, 14074000, "WB9HLA", "FT8", -5, null, null, 14, 43);
        }

        [Fact]
        public void Test_6a50a268_0d87_4b3d_952e_f05b7fe50c78()
        {
            ClusterSpot.TryParse("DX de GM0KTH-#:  10136.0  F5RRS        FT8  +19 dB  685 Hz            1442Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "GM0KTH", null, 10136000, "F5RRS", "FT8", 19, null, null, 14, 42, hzOffset: 685);
        }

        [Fact]
        public void Test_6a50a268_0d87_4b3d_952e_f05b7fe50c79()
        {
            ClusterSpot.TryParse("DX de GM0KTH-#:  10136.0  F5RRS        FT8  + 9 dB  685 Hz            1442Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "GM0KTH", null, 10136000, "F5RRS", "FT8", 9, null, null, 14, 42, hzOffset: 685);
        }

        [Fact]
        public void Test_FT8_with_no_info()
        {
            ClusterSpot.TryParse("DX de K6MKF:      3574.5  HS0ZEE       FT8                            1529Z", out var spot).Should().BeTrue();
            AssertSpot(spot, "K6MKF", null, 3574500, "HS0ZEE", "FT8", null, null, null, 15, 29);
        }

        private static void AssertSpot(ClusterSpot spot, string spotter, int? ssid, long frequency, string spotted, string mode, int? db, int? wpm, string comment, int hours, int mins, int hzOffset = 0)
        {
            spot.ReceiverCallsign.Should().Be(spotter);
            spot.ReceiverSsid.Should().Be(ssid);
            spot.Frequency.Should().Be(frequency + hzOffset);
            spot.SenderCallsign.Should().Be(spotted);
            spot.Mode.Should().Be(mode);
            spot.Snr.Should().Be(db);
            spot.Wpm.Should().Be(wpm);
            spot.Comment.Should().Be(comment);
            spot.TimestampZ.Should().Be(DateTime.UtcNow.Date.AddHours(hours).AddMinutes(mins));
        }
    }
}
