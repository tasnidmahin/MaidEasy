-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 21, 2020 at 05:56 PM
-- Server version: 10.3.15-MariaDB
-- PHP Version: 7.3.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `maideasy`
--

-- --------------------------------------------------------

--
-- Table structure for table `admin`
--

CREATE TABLE `admin` (
  `AdminId` int(20) NOT NULL,
  `username` varchar(70) NOT NULL,
  `password` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `contactus`
--

CREATE TABLE `contactus` (
  `Id` int(20) NOT NULL,
  `Name` varchar(30) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Subject` varchar(100) NOT NULL,
  `Message` varchar(300) NOT NULL,
  `Review` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `contactus`
--

INSERT INTO `contactus` (`Id`, `Name`, `Email`, `Subject`, `Message`, `Review`) VALUES
(1, 'Tasnid Mahin', 'mahin@example.com', 'Test', 'Test', 'Good'),
(2, 'Mahin', 'test@test.com', 'test', 'fg fgrrer ewe ', 'good'),
(3, 'Mahin', 'sample@mail.com', 'test', 'ggg', 'check'),
(4, 'test', 'test@mail.com', 'd', 'msg', 'dsds');

-- --------------------------------------------------------

--
-- Table structure for table `contracts`
--

CREATE TABLE `contracts` (
  `Id` int(20) NOT NULL,
  `WorkerId` int(20) NOT NULL,
  `WorkerName` varchar(40) NOT NULL,
  `UserId` int(20) NOT NULL,
  `StartMonth` varchar(10) NOT NULL,
  `EndMonth` varchar(10) NOT NULL,
  `startTime` varchar(30) NOT NULL,
  `endTime` varchar(30) NOT NULL,
  `Amount` int(10) NOT NULL,
  `Worklist` varchar(300) NOT NULL,
  `status` varchar(15) NOT NULL DEFAULT 'current'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `contracts`
--

INSERT INTO `contracts` (`Id`, `WorkerId`, `WorkerName`, `UserId`, `StartMonth`, `EndMonth`, `startTime`, `endTime`, `Amount`, `Worklist`, `status`) VALUES
(15, 2, 'Even ', 4, ' 07/2020 ', ' 09/2020 ', ' 7:00 AM', ' 8:00 AM ', 150, 'Washing Clothes\n ', 'current'),
(16, 15, 'Tashreef ', 4, ' 09/2020 ', ' 10/2020 ', ' 6:30 AM', ' 7:30 AM', 150, 'Dish Washing\n ', 'current'),
(19, 24, 'Mahin ', 9, ' 09/2020 ', ' 10/2020 ', ' 6:00 AM ', ' 7:00 AM ', 270, 'Dish Washing\nClothing\n ', 'current'),
(21, 24, 'Mahin ', 9, ' 09/2020 ', ' 10/2020 ', ' 7:30 AM ', ' 8:30 AM ', 320, 'Cooking\nClothing\n ', 'current'),
(23, 10, 'Sadman ', 43, ' 09/2020 ', '09/2020', ' 6:00 AM ', ' 6:00 PM ', 6000, ' ', 'previous'),
(24, 10, 'Sadman ', 43, ' 09/2020 ', '09/2020', ' 6:00 AM ', ' 6:00 PM ', 6000, ' ', 'previous'),
(25, 10, 'Sadman ', 43, ' 09/2020 ', '09/2020', ' 6:00 AM ', ' 6:00 PM ', 6000, ' ', 'previous'),
(26, 10, 'Sadman ', 43, ' 09/2020 ', '09/2020', ' 6:00 AM ', ' 6:00 PM ', 6000, ' ', 'previous'),
(27, 10, 'Sadman', 43, '09/2020', '09/2020', '6:00 AM', '6:00 PM', 6000, '', 'previous');

-- --------------------------------------------------------

--
-- Table structure for table `thana`
--

CREATE TABLE `thana` (
  `ThanaId` int(6) NOT NULL,
  `Name` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `thana`
--

INSERT INTO `thana` (`ThanaId`, `Name`) VALUES
(1, 'Adabar'),
(2, 'Azampur'),
(3, 'Badda'),
(4, 'Bangsal'),
(5, 'Bimanbandar'),
(6, 'Cantonment'),
(7, 'Chowkbazar'),
(8, 'Darus Salam'),
(9, 'Demra'),
(10, 'Dhanmondi'),
(11, 'Gendaria'),
(12, 'Gulshan'),
(13, 'Hazaribagh'),
(14, 'Kadamtali'),
(15, 'Kafrul'),
(16, 'Kalabagan'),
(17, 'Kamrangirchar'),
(18, 'Khilgaon'),
(19, 'Khilkhet'),
(20, 'Kotwali'),
(21, 'Lalbagh'),
(22, 'Mirpur'),
(23, 'Mohammadpur'),
(24, 'Motijheel'),
(25, 'New Market'),
(26, 'Pallabi'),
(27, 'Paltan'),
(28, 'Panthapath'),
(29, 'Ramna'),
(30, 'Rampura'),
(31, 'Sabujbagh'),
(32, 'Shah Ali'),
(33, 'Shahbag'),
(34, 'Sher-e-Bangla Nagar'),
(35, 'Shyampur'),
(36, 'Sutrapur'),
(37, 'Tejgaon Industrial Area'),
(38, 'Tejgaon'),
(39, 'Turag'),
(40, 'Uttar Khan'),
(41, 'Uttara'),
(42, 'Vatara'),
(43, 'Wari');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `UserId` int(20) NOT NULL,
  `username` varchar(70) NOT NULL,
  `password` varchar(100) NOT NULL,
  `type` varchar(20) NOT NULL DEFAULT 'general',
  `Name` varchar(30) NOT NULL,
  `mobile` varchar(15) NOT NULL,
  `PresentAddress` varchar(100) NOT NULL,
  `PermanentAddress` varchar(100) NOT NULL,
  `image` varchar(80) DEFAULT 'default',
  `thana` varchar(300) NOT NULL DEFAULT '00000000000000000000000000000000000000000000000000'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`UserId`, `username`, `password`, `type`, `Name`, `mobile`, `PresentAddress`, `PermanentAddress`, `image`, `thana`) VALUES
(1, 'tasnidmahin', '8Jziu8XjaryBIP/F+yK4hxx79JIlva3sbMuHPUYmw/sR6NlV', 'admin', ' Mahin ', ' +8801536174793', ' dhaka ', ' dhaka ', 'default.jpg', '000000000000000000000000000000000000000000100'),
(2, 'tasnidmahin1', 'AcZVFVfpc6ctKEHmFlUOoBEg3Xj9QLrog34BXxRhZ4ERFHOH ', 'general', ' Tasnid Mahin ', ' +8801536174793', ' dhaka ', ' dhaka', 'default.jpg', '000000000000000000000000000000000000000000010'),
(3, 'tasnidmahin2', '1ljs9Z4iwxk+2WWSK8/vv6TgSYYXGAJS5Z4fnKhTSObj2f4c ', 'general', ' Mahin ', ' +8801536174793', ' dhaka ', ' dhaka', 'default.jpg', '000000001000000000000000000000000000000000000'),
(4, 'tasnidmahin3', 'hIa7rz6PQ+78EZ04bUCjBey5KDEOSVPTakfUUfg6FWE2kt2d', 'general', ' Mahin', ' +8801536174793', ' dhaka ', ' dhaka', 'tasnidmahin3.jpg', '00100000000000000000000000000000000000000000000000'),
(6, 'taqi', '/AfBx1Qnzo0KmWTfmepbBKF/9+SQt2KvQa1Ly1zrCQTgQ9EX ', 'admin', ' Taqi ', ' +8801536174793', ' ctg ', ' ctg', 'default.jpg', '01000000000000000000000000000000000000000000000000 '),
(9, 'a', 'ty+iwOJLxZLT3o5oGAvk8I6njYNOgNaM5t7Skseg5OMxyqHa', 'general', ' Mahinnnna', ' +8801536174793', ' dhaka ', ' dhaka', 'default.jpg', '00000100000000000000000000000000000000000000000000'),
(11, 'path', 'crrpeoGembfkFTqZR3VZIMY8yHkWL0z/zuKRChNZmkigrBBQ ', 'general', ' path ', ' +8801536174793', ' a ', ' aa', 'default.jpg', '00000100000000000000000000000000000000000000000000 '),
(39, 'Ami', 'lTak/i2aIuwtl9V0P+TemDCdsrohnn9u6Ke9emf82sbYdJ7v ', 'general', ' Ami ', ' +8801536174793', ' a ', ' a', 'Ami.PNG', '01000000000000000000000000000000000000000000000000 '),
(40, 'admin', 'eSvYlNptkJb6l29oPFdoPeREjuSO3n9OunHwTe0GasNMkNJj ', 'super', ' admin ', ' +8801536174793', ' dhaka ', ' dhaka', 'default.jpg', '00000000000000000100000000000000000000000000000000 '),
(41, 'b', 'ke1tLMOf31Gm3I9Rl0zWdYSSR+dRSBOCt1dJ1EQQ/B6HWRUZ ', 'general', ' Mahin ', ' +8801536174793', ' dhaka ', ' dhaka', 'default.jpg', '00000000000000000000000000000010000000000000000000 '),
(42, 'c', '1KtM1DVULs0FWZebEUlECpqmPA65m2kvX2JdJ+0HvBdYGcSP', 'general', ' Mahin ', ' +8801536174793', ' dhaka ', ' sylhet', 'default.jpg', '00000000000000000000000000000000000000000010000000'),
(43, 'User A', 'zj4fcLKoQ75XSiSfUWxHgf68O89tV04zkmywcF00q8McuMjU ', 'blocked', ' user a ', ' +8801536174793', ' dhaka ', ' dhaka', 'default.jpg', '00000001000000000000000000000000000000000000000000 '),
(44, 'User B', 'oLM8kQOk018D/kHVlXZUr60MSXTS9lFPGUVHEugWn9nhxuq7 ', 'general', ' user b ', ' +8801536174793', ' dhaka ', ' dhaka', 'default.jpg', '00000000000000000000100000000000000000000000000000 '),
(45, 'User C', '8swG5LVBfzeyJaD5U+/oloexOS6T4JLelbs/JN/S6y5ee7mR ', 'general', ' user c ', ' +8801536174793', ' dhaka ', ' dhaka', 'default.jpg', '00000000000000000000000100000000000000000000000000 '),
(46, 'User D', '3r58c+UrtOQRwNlx5uInxfWmxuIrfXwhrxYHTtENtGvImoU2 ', 'general', ' user d ', ' +8801536174793', ' dhaka ', ' dhaka', 'default.jpg', '00000000000000000000000000000000010000000000000000 '),
(47, 'User E', 'ihreBvr/f1ZIYn3oKJe+/ptH0MJUS6VeGoESXAG/XR/h+RdK ', 'blocked', ' user e ', ' +8801536174793', ' dhaka ', ' dhaka', 'default.jpg', '00000000000000000000010000000000000000000000000000 '),
(51, 'User F', 'tXvZycxORbwiYZ8fbdrLiZODngprQUx3nBIUMY40bDH+exp+ ', 'general', ' user f ', ' +8801536174793', ' dhaka ', ' dhaka', 'default.jpg', '00000000000000000000010000000000000000000000000000 ');

-- --------------------------------------------------------

--
-- Table structure for table `work`
--

CREATE TABLE `work` (
  `WorkId` int(6) NOT NULL,
  `Name` varchar(30) NOT NULL,
  `UnitPrice` int(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `work`
--

INSERT INTO `work` (`WorkId`, `Name`, `UnitPrice`) VALUES
(1, 'Permanent', 5000),
(2, 'Baby Care', 6000),
(3, 'Elderly Care', 7000),
(4, 'Cooking', 200),
(5, 'Dish Washing', 150),
(6, 'Clothing', 120);

-- --------------------------------------------------------

--
-- Table structure for table `worker`
--

CREATE TABLE `worker` (
  `WorkerId` int(20) NOT NULL,
  `Name` varchar(30) NOT NULL,
  `fatherName` varchar(30) NOT NULL,
  `mobile` varchar(15) NOT NULL,
  `PresentAddress` varchar(100) NOT NULL,
  `PermanentAddress` varchar(100) NOT NULL,
  `Area` varchar(300) NOT NULL DEFAULT '00000000000000000000000000000000000000000000000000',
  `type` varchar(8) NOT NULL,
  `experience` int(10) NOT NULL DEFAULT 0,
  `joinDate` varchar(30) NOT NULL,
  `updateStatus` varchar(30) NOT NULL DEFAULT 'pending',
  `rating` double(10,2) DEFAULT 0.00,
  `image` varchar(80) DEFAULT NULL,
  `status` varchar(25) NOT NULL DEFAULT '0000000000000000000000000',
  `gender` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `worker`
--

INSERT INTO `worker` (`WorkerId`, `Name`, `fatherName`, `mobile`, `PresentAddress`, `PermanentAddress`, `Area`, `type`, `experience`, `joinDate`, `updateStatus`, `rating`, `image`, `status`, `gender`) VALUES
(1, 'Jim', 'Jim F', '01701040010', 'dhaka', 'dhaka', '1110000000000110000000000000000000000000000', '1000', 1, '', 'pending', 0.00, '1.jpg', '0000000000000000000000000', 'Male'),
(2, 'Even', 'Even F', '01701040020', 'canada', 'dhaka', '0011111000000000000000000000000000000000000', '1000', 1, '', 'pending', 2.00, 'defaultmaid.png', '0011100000000000000000000', 'Male'),
(3, 'Dip', 'Dip F', '01701040030', 'dhaka', 'rangamati', '1100000000000000000000000000000000000000000', '1000', 1, '', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Male'),
(4, 'Umme', 'Umme F', '01701040040', 'dhaka', 'dhaka', '0000000000000000001100000000000000000000000', '0001', 1, '', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Female'),
(5, 'Shams', 'ShamsF', '01701040050', 'dhaka', 'dhaka', '1111100000000000010000000000000000000000000', '0100', 1, '', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Male'),
(7, 'Dhusor', 'Dhusor F', '01701040050', 'dhaka', 'noakhali', '0011111110111000000111111111110001110000111', '0100', 1, '', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Male'),
(8, 'Joy', 'Joy F', '01701040070', 'dhaka', 'rangamati', '1111111000000011000000000000000000000001111', '0001', 1, '', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Male'),
(9, 'Farhan', 'Farhan F', '01701040080', 'dhaka', 'dhaka', '0101111001110000111100000111001111000000000', '0001', 1, '', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Male'),
(10, 'Sadman', 'Sadman F', '01701040090', 'dhaka', 'mymensing', '1111111111111111111111111111111111111011111', '0010', 1, '', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Male'),
(12, 'Sabbir', 'Sabbir F ', ' 01701040110 ', ' dhaka ', ' dhaka ', '0000000000000110000001000000000000000000011', '1000', 1, '08/2020 ', 'pending', 0.00, '12.jpg', '0000000000000000000000000', 'Female'),
(13, 'Shah Alam', 'Tasin F ', ' 01701040120 ', ' dhaka ', ' kushtia ', '0000000001000000000001100000000000000000000', '0100', 1, '08/2020 ', 'pending', 0.00, '13.png', '0000000000000000000000000', 'Male'),
(14, 'Tanim', 'Tanim F ', ' +8801701040130', ' dhaka ', ' dhaka ', '0000001100001000000000000000000000000000000', '1000', 1, '08/2020 ', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', ' Male'),
(15, 'Tashreef', 'Tashreef F ', ' +8801701040140', ' dhaka ', ' Mymensing ', '0010000000000000010000010000100000001100000', '1000', 0, '09/2020 ', 'pending', 4.00, 'defaultmaid.png', '0111000000000000000000000', ' Male'),
(16, 'Taqi', 'Taqi F ', ' +8801701040150', ' dhaka ', ' Chottogram ', '0000001000110000000100000000010000001100000', '0100', 0, '09/2020 ', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', ' Male'),
(17, 'Tamhid', 'Tahmid F', '+8801701040150', 'dhaka', 'Chottogram', '0010101010011010000000000001100000000000000', '0100', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Male'),
(18, 'Nishat', 'Nishat F', '+8801701040160', 'dhaka', 'Rajshahi', '0000000000001010000000110110000001100000000', '0110', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Female'),
(22, 'Muhu', 'Muhu F', '+8801701040170', 'dhaka', 'dhaka', '0000000000110000000010000110000001010000000', '0001', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Female'),
(23, 'Aesha', 'Aesha F', '+8801701040180', 'dhaka ', 'rajshahi', '0000101000010000000000011100000000010000000', '0100', 0, '09/2020', 'pending', 0.00, '23.jpg', '0000000000000000000000000', 'Female'),
(24, 'Mahin', 'Mahin', '+8801701040190', 'dhaka', 'dhaka', '1111011101001110100000000000110000000001111', '1000', 0, '09/2020', 'pending', 1.75, 'defaultmaid.png', '1111110000000000000000000', 'Male'),
(25, 'A', 'A Father ', '+8801701040150', 'dhaka', 'dhaka', '0011101001010010000000000000101000001001101', '0100', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Male'),
(26, 'B', 'B Father', '+8801701040150', 'dhaka', 'dhaka', '1110000111000001110100000011100000000100100', '1000', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Female'),
(27, 'C', 'C father', '+8801701040190', 'dhaka', 'Mymensing', '0000000011000000000110000001110000011010110', '1000', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Male'),
(28, 'D', 'D Father', '+8801701040190', 'dhaka', 'comilla', '1111100111111101101000000011101000000001100', '0010', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Female'),
(29, 'E', 'E Father', '+8801701040190', 'dhaka', 'kushtia', '0001111110110000000001000000000000000000000', '1000', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Female'),
(30, 'F', 'F Father', '+8801701040190', 'dhaka', 'rangpur', '1110101010101110101110010000000101101001000', '0001', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Female'),
(31, 'G', 'G Father', '+8801701040090', 'dhaka', 'Sylhet', '0001010011100000001100000110110000000000100', '1000', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Male'),
(32, 'H', 'H Father', '+8801701040190', 'dhaka', 'khulna', '0000111001100000001100000000000000011101110', '1000', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Female'),
(33, 'I', 'I Father', '+8801701040200', 'dhaka', 'gazipur', '1111101101000110000000001110100000000000010', '0010', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Female'),
(34, 'J', 'J Father', '+8801701040110', 'dhaka', 'feni', '1111011010000001110000000011110000000100110', '0001', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Female'),
(35, 'K', 'K Father', '+8801701040190', 'dhaka', 'dhaka', '1110000001110000000000000110001001110001001', '1000', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Female'),
(36, 'L', 'L Father', '+8801701040110', 'dhaka', 'chiitagong', '0000000000011100100111010000011001100000110', '0010', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Female'),
(37, 'M', 'M Father', '+8801701040190', 'dhaka', 'dhaka', '1110000111000000001000111110000000001110000', '0010', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Female'),
(38, 'N', 'N Father', '+8801701040200', 'dhaka', 'rajshahi', '0011011111000000000110000000001110001000010', '0010', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Female'),
(39, 'O', 'O Father', '+8801701040190', 'dhaka', 'dhaka', '0100100110100000001110000100001010100100010', '0001', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Female'),
(40, 'Swaad', 'Swaad F', '+8801701040200', 'dhaka', 'dhaka', '1111111110000000000000011000000000000000100', '1000', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Male'),
(41, 'P', 'P Father', '+8801701040770', 'dhaka', 'dhaka', '0001001001001000000000101100000000011001000', '0001', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Female'),
(42, 'Raju', 'Raju F', '+8801701040770', 'dhaka', 'dhaka', '1101110001010100000000000011000001100000100', '1000', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Male'),
(43, 'Mahi', 'Mahi F', '+8801701040190', 'dhaka', 'comilla', '1110000000011110100110000000001110100011000', '1000', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Male'),
(44, 'Q', 'Q Father', '+8801701042600', 'dhaka', 'dhaka', '1101010000100011110000000000111101001000000', '0001', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Female'),
(45, 'Q', 'Q Father', '+8801701040150', 'dhaka', 'dhaka', '1011010100110000000000001010000001001100000', '1000', 0, '09/2020', 'pending', 0.00, 'defaultmaid.png', '0000000000000000000000000', 'Male');

-- --------------------------------------------------------

--
-- Table structure for table `workerreview`
--

CREATE TABLE `workerreview` (
  `Id` int(20) NOT NULL,
  `WorkerId` int(20) NOT NULL,
  `username` varchar(70) NOT NULL,
  `rating` double(10,2) DEFAULT 0.00,
  `description` varchar(250) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `workerreview`
--

INSERT INTO `workerreview` (`Id`, `WorkerId`, `username`, `rating`, `description`) VALUES
(1, 2, 'tasnidmahin3', 1.50, 'Fakibaj '),
(2, 2, 'tasnidmahin3', 2.50, 'tarahura kore khali '),
(4, 15, 'tasnidmahin3', 4.00, 'Punctual '),
(5, 15, 'tasnidmahin3', 4.00, 'Hard working '),
(6, 24, 'a', 3.50, ' '),
(7, 24, 'a', 0.00, 'dd ');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `admin`
--
ALTER TABLE `admin`
  ADD PRIMARY KEY (`AdminId`),
  ADD UNIQUE KEY `username` (`username`);

--
-- Indexes for table `contactus`
--
ALTER TABLE `contactus`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `contracts`
--
ALTER TABLE `contracts`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `WorkerId` (`WorkerId`),
  ADD KEY `UserId` (`UserId`);

--
-- Indexes for table `thana`
--
ALTER TABLE `thana`
  ADD PRIMARY KEY (`ThanaId`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`UserId`),
  ADD UNIQUE KEY `username` (`username`);

--
-- Indexes for table `work`
--
ALTER TABLE `work`
  ADD PRIMARY KEY (`WorkId`);

--
-- Indexes for table `worker`
--
ALTER TABLE `worker`
  ADD PRIMARY KEY (`WorkerId`);

--
-- Indexes for table `workerreview`
--
ALTER TABLE `workerreview`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `WorkerId` (`WorkerId`),
  ADD KEY `fk_WorkerReview` (`username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `admin`
--
ALTER TABLE `admin`
  MODIFY `AdminId` int(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `contactus`
--
ALTER TABLE `contactus`
  MODIFY `Id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `contracts`
--
ALTER TABLE `contracts`
  MODIFY `Id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT for table `thana`
--
ALTER TABLE `thana`
  MODIFY `ThanaId` int(6) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=48;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `UserId` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=52;

--
-- AUTO_INCREMENT for table `work`
--
ALTER TABLE `work`
  MODIFY `WorkId` int(6) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `worker`
--
ALTER TABLE `worker`
  MODIFY `WorkerId` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=46;

--
-- AUTO_INCREMENT for table `workerreview`
--
ALTER TABLE `workerreview`
  MODIFY `Id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `contracts`
--
ALTER TABLE `contracts`
  ADD CONSTRAINT `contracts_ibfk_1` FOREIGN KEY (`WorkerId`) REFERENCES `worker` (`WorkerId`),
  ADD CONSTRAINT `contracts_ibfk_2` FOREIGN KEY (`UserId`) REFERENCES `users` (`UserId`);

--
-- Constraints for table `workerreview`
--
ALTER TABLE `workerreview`
  ADD CONSTRAINT `fk_WorkerReview` FOREIGN KEY (`username`) REFERENCES `users` (`username`),
  ADD CONSTRAINT `workerreview_ibfk_1` FOREIGN KEY (`WorkerId`) REFERENCES `worker` (`WorkerId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
