-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Hostiteľ: 127.0.0.1
-- Čas generovania: Sun 12.Dec 2021, 15:04
-- Verzia serveru: 10.4.21-MariaDB
-- Verzia PHP: 8.0.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Databáza: `movies`
--

-- --------------------------------------------------------

--
-- Štruktúra tabuľky pre tabuľku `movies`
--

CREATE TABLE `movies` (
  `Id` int(11) NOT NULL,
  `Name_Movie` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `Name_Director` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `Main_Actor` varchar(50) NOT NULL,
  `Rating_Imdb` decimal(10,1) NOT NULL,
  `Added_By_User` varchar(50) NOT NULL,
  `Update_By_User` varchar(50) NOT NULL,
  `Node_Created` varchar(50) NOT NULL,
  `Node_Update` varchar(50) NOT NULL,
  `When_Created` datetime NOT NULL,
  `When_Modify` datetime NOT NULL,
  `status` enum('1','0') CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL DEFAULT '1' COMMENT '1=Active, 0=Inactive'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Sťahujem dáta pre tabuľku `movies`
--

INSERT INTO `movies` (`Id`, `Name_Movie`, `Name_Director`, `Main_Actor`, `Rating_Imdb`, `Added_By_User`, `Update_By_User`, `Node_Created`, `Node_Update`, `When_Created`, `When_Modify`, `status`) VALUES
(1, 'Nehanební bastardi', 'Quentin Tarantino', ' Brad Pitt', '8.3', 'samuel', '', '25.19.51.14', '', '2021-12-06 20:21:03', '2021-12-06 20:21:03', '1'),
(3, 'Vykúpenie ', ' Frank Darabont', 'Tim Robbins', '9.3', 'samuel', 'martin', '25.19.51.14', '25.69.87.199', '2021-12-06 20:24:48', '2021-12-06 20:38:20', '1'),
(5, 'Duna', ' Denis Villeneuve', 'Timothée Chalamet', '8.2', 'samuel', '', '25.35.50.147', '', '2021-12-06 20:42:00', '2021-12-06 20:42:00', '1'),
(6, 'Posledný súboj', 'Ridley Scott', 'Matt Damon', '7.5', 'martin', '', '25.42.132.140', '', '2021-12-06 20:43:12', '2021-12-06 20:43:12', '1'),
(7, 'Interstellar', 'Christoper Nolan', 'Mattew ', '74.0', 'martin', 'martin', '25.69.87.199', '25.69.87.199', '2021-12-06 20:53:35', '2021-12-06 20:54:19', '1'),
(8, 'Interstellar 2 ', 'Christoper Nolan', 'Mattew ', '11.0', 'martin', '', '25.35.50.147', '', '2021-12-06 21:54:02', '2021-12-06 21:54:02', '1'),
(10, 'Free guy', 'Shawn Levy', 'M. Lieberman', '4.0', 'martin', '', '25.42.132.140', '', '2021-12-07 20:09:42', '2021-12-07 20:09:42', '1'),
(11, 'Deadpool', 'David Leitch', 'Rhett Reese', '3.0', 'martin', 'samuel', '25.69.87.199', '25.35.50.147', '2021-12-07 20:48:32', '2021-12-08 08:38:42', '1'),
(12, 'Sam Doma 2', 'C columbus', 'M culcin', '9.0', 'martin', 'samuel', '25.69.87.199', '25.35.50.147', '2021-12-08 09:53:28', '2021-12-08 09:54:10', '1'),
(13, 'Maska 2', 'C. Russel', 'J. Carrey', '8.0', 'martin', 'martin', '25.69.87.199', '25.42.132.140', '2021-12-08 09:56:23', '2021-12-08 09:56:42', '1');

-- --------------------------------------------------------

--
-- Štruktúra tabuľky pre tabuľku `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(255) NOT NULL,
  `created_at` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Sťahujem dáta pre tabuľku `users`
--

INSERT INTO `users` (`id`, `username`, `password`, `created_at`) VALUES
(1, 'samuel', '$2y$10$W9HIN15DZ5NYTXog3xdIrOwTsF09X83abjAhXJO5yYYaNnvEWqWX6', '2021-12-06 20:17:23'),
(2, 'martin', '$2y$10$ug4zY8RwIevsbyTreX8shuRPI7784Kxa0OnCjatrbwwWj1mIiuN/e', '2021-12-06 20:18:17'),
(3, 'fero', '$2y$10$0oV8vMNngaWieFKepaxN6OCyRwbtAPzilh4d9lMk2zsoRu1de0v/O', '2021-12-06 21:22:37'),
(4, 'tomas', '$2y$10$sFLAayUbcepQT.GFrUjC0ueaEGqeAWf7QlvLIcx6BfIQOuBv6sN3q', '2021-12-07 10:58:53'),
(5, 'peto', '$2y$10$ijL9PvzjEnnpD7CNc1wdbu0p1Rhen/Eqrzs/.tXN7vzhzhAWSHSy2', '2021-12-07 20:13:20'),
(6, 'jakub', '$2y$10$E5me1d0.ewfrCdH9PofQ3e7DbZFP9kkxxkslqpdmHYKLxoNXM/BJq', '2021-12-07 21:12:29'),
(7, 'lubo', '$2y$10$3ud3fwO.uVvJes4lYDXhb.1Rx3Odd6r8niM3nz0lj8ckDZBb0Nm7i', '2021-12-07 21:15:08'),
(8, 'adam', '$2y$10$S6CaIIqdnubohbWNj4wqe.e3Fv05eUhLZsq.wt0UE.XDTwbAbjc5a', '2021-12-08 09:47:49'),
(9, 'rasto', '$2y$10$fY.JAQmlrhJQm3qOW3LW/ONj19wte3pJPm6zd9ee/cwi7dXSuPhc2', '2021-12-08 09:51:01'),
(10, 'matko', '$2y$10$XMzFnaVyMZ736mQimwQ0seAU39bwomqTtZEQJ59zoBLypxlXst3Ca', '2021-12-08 14:49:15'),
(11, 'matus', '$2y$10$aTydLoHvTCc4.j9ugYfyuu9.yd1x52i7Zaw9jAHfVnirM09PAtQd.', '2021-12-08 14:50:59'),
(12, 'matko1', '$2y$10$g7zwGZgb2lmA3a0YPnQ1JORZwZKdjXnRqxgF3KhQKaZztpO2lCK1m', '2021-12-08 15:05:08');

--
-- Kľúče pre exportované tabuľky
--

--
-- Indexy pre tabuľku `movies`
--
ALTER TABLE `movies`
  ADD PRIMARY KEY (`Id`);

--
-- Indexy pre tabuľku `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- AUTO_INCREMENT pre exportované tabuľky
--

--
-- AUTO_INCREMENT pre tabuľku `movies`
--
ALTER TABLE `movies`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT pre tabuľku `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
