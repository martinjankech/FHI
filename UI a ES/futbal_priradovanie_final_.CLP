(deftemplate tim
(multislot nazov (type SYMBOL))
(slot liga(type INTEGER)(allowed-values 1 2 3 ))
(slot krajina (type SYMBOL)(allowed-values Slovensko Cesko Polsko))
(slot tim_poziadavka_post (type SYMBOL) (allowed-values brankar obranca zaloznik utocnik))
(slot tim_poziadavka_vek_od (type INTEGER))
(slot tim_poziadavka_vek_do (type INTEGER))
(slot tim_poziadavka_celkovy_vykon_od (type INTEGER)(default 0))
(slot ponukany_plat(type INTEGER))
)

(deftemplate hrac
(multislot meno(type SYMBOL))
(slot vek (type INTEGER))
(slot post (type SYMBOL)(allowed-values brankar obranca zaloznik utocnik))
(slot najvyssia_liga(type INTEGER)(allowed-values 1 2 3))
(slot celkovy_vykon(type INTEGER))
(slot hrac_krajina1(type SYMBOL))
(slot hrac_krajina2 (type SYMBOL))
(slot hrac_plat(type INTEGER))
(slot zhoda (type INTEGER)(default 0))
)
(deffacts timy
(tim(nazov Slovan Bratislava)(liga 1)(krajina Slovensko)(tim_poziadavka_post utocnik)(tim_poziadavka_vek_od 21)(tim_poziadavka_vek_do 25)(tim_poziadavka_celkovy_vykon_od 90)(ponukany_plat 3500))
(tim(nazov Dunajsk· Streda)(liga 1)(krajina Slovensko)(tim_poziadavka_post brankar)(tim_poziadavka_vek_od 18)(tim_poziadavka_vek_do 21)(tim_poziadavka_celkovy_vykon_od 80)(ponukany_plat 4000))
(tim(nazov Zilina)(liga 1)(krajina Slovensko)(tim_poziadavka_post obranca)(tim_poziadavka_vek_od 25)(tim_poziadavka_vek_do 30)(tim_poziadavka_celkovy_vykon_od 90)(ponukany_plat 2500))
(tim(nazov Sparta Praha)(liga 1)(krajina Cesko)(tim_poziadavka_post zaloznik)(tim_poziadavka_vek_od 27)(tim_poziadavka_vek_do 29)(tim_poziadavka_celkovy_vykon_od 80)(ponukany_plat 4500))
(tim(nazov Plzen)(liga 1)(krajina Cesko)(tim_poziadavka_post utocnik)(tim_poziadavka_vek_od 18)(tim_poziadavka_vek_do 30)(tim_poziadavka_celkovy_vykon_od 85)(ponukany_plat 3500))
(tim(nazov Pardubice)(liga 1)(krajina Cesko)(tim_poziadavka_post obranca)(tim_poziadavka_vek_od 21)(tim_poziadavka_vek_do 38)(tim_poziadavka_celkovy_vykon_od 65)(ponukany_plat 2800))
(tim(nazov Legia)(liga 1)(krajina Polsko)(tim_poziadavka_post zaloznik)(tim_poziadavka_vek_od 28)(tim_poziadavka_vek_do 35)(tim_poziadavka_celkovy_vykon_od 85)(ponukany_plat 2400))
(tim(nazov Cracovia)(liga 1)(krajina Polsko)(tim_poziadavka_post utocnik)(tim_poziadavka_vek_od 18)(tim_poziadavka_vek_do 21)(tim_poziadavka_celkovy_vykon_od 68)(ponukany_plat 1800))
(tim(nazov Rakow)(liga 1)(krajina Polsko)(tim_poziadavka_post brankar)(tim_poziadavka_vek_od 20)(tim_poziadavka_vek_do 29)(tim_poziadavka_celkovy_vykon_od 80)(ponukany_plat 2750))
(tim(nazov Banska Bystrica)(liga 2)(krajina Slovensko)(tim_poziadavka_post obranca)(tim_poziadavka_vek_od 21)(tim_poziadavka_vek_do 40)(tim_poziadavka_celkovy_vykon_od 60)(ponukany_plat 1850))
(tim(nazov FC Koöice)(liga 2)(krajina Slovensko)(tim_poziadavka_post utocnik)(tim_poziadavka_vek_od 18)(tim_poziadavka_vek_do 29)(tim_poziadavka_celkovy_vykon_od 560)(ponukany_plat 2000))
(tim(nazov Poprad)(liga 2)(krajina Slovensko)(tim_poziadavka_post zaloznik)(tim_poziadavka_vek_od 18)(tim_poziadavka_vek_do 29)(tim_poziadavka_celkovy_vykon_od 80)(ponukany_plat 2100))
(tim(nazov Jihlava)(liga 2)(krajina Cesko)(tim_poziadavka_post brankar)(tim_poziadavka_vek_od 19)(tim_poziadavka_vek_do 31)(tim_poziadavka_celkovy_vykon_od 55)(ponukany_plat 2200))
(tim(nazov Varnsdorf)(liga 2)(krajina Cesko)(tim_poziadavka_post obranca)(tim_poziadavka_vek_od 18)(tim_poziadavka_vek_do 25)(tim_poziadavka_celkovy_vykon_od 65)(ponukany_plat 1650))
(tim(nazov Chrudim)(liga 2)(krajina Cesko)(tim_poziadavka_post utocnik)(tim_poziadavka_vek_od 18)(tim_poziadavka_vek_do 24)(tim_poziadavka_celkovy_vykon_od 80)(ponukany_plat 1450))
(tim(nazov Polkowice)(liga 2)(krajina Polsko)(tim_poziadavka_post zaloznik)(tim_poziadavka_vek_od 22)(tim_poziadavka_vek_do 22)(tim_poziadavka_celkovy_vykon_od 35)(ponukany_plat 1350))
(tim(nazov Katowice)(liga 2)(krajina Polsko)(tim_poziadavka_post zaloznik)(tim_poziadavka_vek_od 21)(tim_poziadavka_vek_do 25)(tim_poziadavka_celkovy_vykon_od 65)(ponukany_plat 1550 ))
(tim(nazov Lublin)(liga 2)(krajina Polsko)(tim_poziadavka_post obranca)(tim_poziadavka_vek_od 30)(tim_poziadavka_vek_do 26)(tim_poziadavka_celkovy_vykon_od 70)(ponukany_plat 1450))
(tim(nazov Myjava)(liga 3)(krajina Slovensko)(tim_poziadavka_post utocnik)(tim_poziadavka_vek_od 29)(tim_poziadavka_vek_do 28)(tim_poziadavka_celkovy_vykon_od 65)(ponukany_plat 1550))
(tim(nazov Castkovce)(liga 3)(krajina Slovensko)(tim_poziadavka_post brankar)(tim_poziadavka_vek_od 28)(tim_poziadavka_vek_do 32)(tim_poziadavka_celkovy_vykon_od 35)(ponukany_plat 1250))
(tim(nazov Crystal)(liga 3)(krajina Slovensko)(tim_poziadavka_post zaloznik)(tim_poziadavka_vek_od 25)(tim_poziadavka_vek_do 33)(tim_poziadavka_celkovy_vykon_od 55)(ponukany_plat 1150))
(tim(nazov Galanta)(liga 3)(krajina Slovensko)(tim_poziadavka_post utocnik)(tim_poziadavka_vek_od 23)(tim_poziadavka_vek_do 34)(tim_poziadavka_celkovy_vykon_od 70)(ponukany_plat 1350))
(tim(nazov Domazlice)(liga 3)(krajina Cesko)(tim_poziadavka_post obranca)(tim_poziadavka_vek_od 22)(tim_poziadavka_vek_do 37)(tim_poziadavka_celkovy_vykon_od 50)(ponukany_plat 1550))
(tim(nazov Benesov)(liga 3)(krajina Cesko)(tim_poziadavka_post brankar)(tim_poziadavka_vek_od 18)(tim_poziadavka_vek_do 35)(tim_poziadavka_celkovy_vykon_od 55)(ponukany_plat 950))
(tim(nazov Sokolov)(liga 3)(krajina Cesko)(tim_poziadavka_post obranca)(tim_poziadavka_vek_od 21)(tim_poziadavka_vek_do 24)(tim_poziadavka_celkovy_vykon_od 60)(ponukany_plat 1020))
(tim(nazov Slavia Praha B)(liga 3)(krajina Cesko)(tim_poziadavka_post utocnik)(tim_poziadavka_vek_od 19)(tim_poziadavka_vek_do 28)(tim_poziadavka_celkovy_vykon_od 40)(ponukany_plat 1120))
(tim(nazov Legionowo)(liga 3)(krajina Polsko)(tim_poziadavka_post obranca)(tim_poziadavka_vek_od 18)(tim_poziadavka_vek_do 35)(tim_poziadavka_celkovy_vykon_od 60)(ponukany_plat 1450))
(tim(nazov Radomsko)(liga 3)(krajina Polsko)(tim_poziadavka_post zaloznik)(tim_poziadavka_vek_od 23)(tim_poziadavka_vek_do 36)(tim_poziadavka_celkovy_vykon_od 50)(ponukany_plat 850))
(tim(nazov Swit)(liga 3)(krajina Polsko)(tim_poziadavka_post zaloznik)(tim_poziadavka_vek_od 30)(tim_poziadavka_vek_do 40)(tim_poziadavka_celkovy_vykon_od 60)(ponukany_plat 1000))
)

(deffacts hraci
(hrac(meno Adam Bartko)(vek 26)(post utocnik)(najvyssia_liga 3)(celkovy_vykon 55)(hrac_krajina1 Slovensko)(hrac_krajina2 Polsko)( hrac_plat 1000))
(hrac(meno Ladislav Bili)(vek 29)(post obranca)(najvyssia_liga 2)(celkovy_vykon 65)(hrac_krajina1 Polsko)(hrac_krajina2 Cesko)( hrac_plat 1500))
(hrac(meno Kristian Kokolik)(vek 36)(post zaloznik)(najvyssia_liga 2)(celkovy_vykon 72)(hrac_krajina1 Cesko)(hrac_krajina2 Polsko)( hrac_plat 2200))
(hrac(meno Adam Stas)(vek 23)(post utocnik)(najvyssia_liga 1)(celkovy_vykon 89)(hrac_krajina1 Slovensko)(hrac_krajina2 Cesko)( hrac_plat 3750))
(hrac(meno Peter Bosak)(vek 18)(post obranca)(najvyssia_liga 3)(celkovy_vykon 65)(hrac_krajina1 Cesko)(hrac_krajina2 Polsko)( hrac_plat 1250))
(hrac(meno Matej Gdovin)(vek 19)(post utocnik)(najvyssia_liga 2)(celkovy_vykon 69)(hrac_krajina1 Slovensko)(hrac_krajina2 Cesko)( hrac_plat 1450))
(hrac(meno David Maslej)(vek 22)(post zaloznik)(najvyssia_liga 2)(celkovy_vykon 81)(hrac_krajina1 Slovensko)(hrac_krajina2 Cesko)( hrac_plat 2800))
(hrac(meno Pavol Maslej)(vek 38)(post obranca)(najvyssia_liga 1)(celkovy_vykon 92)(hrac_krajina1 Cesko)(hrac_krajina2 Polsko)( hrac_plat 4000))
(hrac(meno Robert Feco)(vek 40)(post utocnik)(najvyssia_liga 1)(celkovy_vykon 93)(hrac_krajina1 Slovensko)(hrac_krajina2 Cesko)( hrac_plat 4300))
(hrac(meno Peter Stapinsky)(vek 35)(post zaloznik)(najvyssia_liga 1)(celkovy_vykon 92)(hrac_krajina1 Slovensko)(hrac_krajina2 Polsko)( hrac_plat 4400))
(hrac(meno Jakub Svist)(vek 25)(post zaloznik)(najvyssia_liga 3)(celkovy_vykon 45)(hrac_krajina1 Cesko)(hrac_krajina2 Polsko)( hrac_plat 890))
(hrac(meno Martin Moravec)(vek 23)(post utocnik)(najvyssia_liga 3)(celkovy_vykon 52)(hrac_krajina1 Slovensko )(hrac_krajina2 Polsko)( hrac_plat 970))
(hrac(meno Stanislav Kazminsky)(vek 28)(post zaloznik)(najvyssia_liga 2)(celkovy_vykon 67)(hrac_krajina1 Cesko )(hrac_krajina2 Polsko)( hrac_plat 1600))
(hrac(meno Jakub Kicuta)(vek 29)(post zaloznik)(najvyssia_liga 2)(celkovy_vykon 74)(hrac_krajina1 Slovensko)(hrac_krajina2 Cesko)( hrac_plat 1300))
(hrac(meno Samuel Hudak)(vek 30)(post obranca)(najvyssia_liga 2)(celkovy_vykon 69)(hrac_krajina1 Slovensko )(hrac_krajina2 Polsko)( hrac_plat 1800))
(hrac(meno Gregor Boncur)(vek 31)(post zaloznik)(najvyssia_liga 2)(celkovy_vykon 78)(hrac_krajina1 Cesko)(hrac_krajina2 Polsko)( hrac_plat 1750))
(hrac(meno Matej Pirschel)(vek 32)(post obranca)(najvyssia_liga 2)(celkovy_vykon 77)(hrac_krajina1 Polsko)(hrac_krajina2 Cesko)( hrac_plat 1350))
(hrac(meno Peter Konecny)(vek 22)(post brankar)(najvyssia_liga 2)(celkovy_vykon 63)(hrac_krajina1 Polsko)(hrac_krajina2 Cesko)( hrac_plat 2100))
(hrac(meno Jozef Jencik)(vek 20)(post obranca)(najvyssia_liga 1)(celkovy_vykon 85)(hrac_krajina1 Slovensko)(hrac_krajina2 Polsko)( hrac_plat 2150))
(hrac(meno Simon Albert)(vek 19)(post utocnik)(najvyssia_liga 1)(celkovy_vykon 84)(hrac_krajina1 Polsko)(hrac_krajina2 Slovensko)( hrac_plat 2350))
(hrac(meno Adam Rybak)(vek 18)(post zaloznik)(najvyssia_liga 1)(celkovy_vykon 82)(hrac_krajina1 Slovensko)(hrac_krajina2 Cesko)( hrac_plat 2120))
(hrac(meno Peter Zubak)(vek 24)(post zaloznik)(najvyssia_liga 1)(celkovy_vykon 81)(hrac_krajina1 Slovensko )(hrac_krajina2 Polsko)( hrac_plat 2080))
(hrac(meno Patrik Papp)(vek 29)(post obranca)(najvyssia_liga 3)(celkovy_vykon 69)(hrac_krajina1 Polsko)(hrac_krajina2 Cesko)( hrac_plat 1690))
(hrac(meno Adam Servetka)(vek 39)(post zaloznik)(najvyssia_liga 2)(celkovy_vykon 70)(hrac_krajina1 Slovensko )(hrac_krajina2 Polsko)( hrac_plat 1600))
(hrac(meno Maximilian Lopuch)(vek 36)(post utocnik)(najvyssia_liga 3)(celkovy_vykon 60)(hrac_krajina1 Cesko)(hrac_krajina2 Slovensko)( hrac_plat 2320))
(hrac(meno Alexander Kling)(vek 33)(post zaloznik)(najvyssia_liga 1)(celkovy_vykon 80)(hrac_krajina1 Slovensko)(hrac_krajina2 Polsko)( hrac_plat 2600))
(hrac(meno Matus Pesta)(vek 32)(post utocnik)(najvyssia_liga 1)(celkovy_vykon 87)(hrac_krajina1 Slovensko)(hrac_krajina2 Polsko)( hrac_plat 3000))
(hrac(meno Samuel Patus)(vek 28)(post zaloznik)(najvyssia_liga 1)(celkovy_vykon 81)(hrac_krajina1 Cesko)(hrac_krajina2 Slovensko)( hrac_plat 2600))
(hrac(meno Frantisek Vestur)(vek 21)(post obranca)(najvyssia_liga 1)(celkovy_vykon 83)(hrac_krajina1 Slovensko)(hrac_krajina2 Polsko)( hrac_plat 2650))
(hrac(meno Peter Hrubizna)(vek 22)(post utocnik)(najvyssia_liga 2)(celkovy_vykon 75)(hrac_krajina1 Polsko)(hrac_krajina2 Cesko)( hrac_plat 1850))
(hrac(meno Jakub Svitana)(vek 34)(post obranca)(najvyssia_liga 2)(celkovy_vykon 79)(hrac_krajina1 Cesko)(hrac_krajina2 Polsko)( hrac_plat 1760))
(hrac(meno Ladislav Molnar)(vek 35)(post utocnik)(najvyssia_liga 2)(celkovy_vykon 75)(hrac_krajina1 Cesko )(hrac_krajina2 Slovensko)( hrac_plat 1750))
(hrac(meno Samuel Spak)(vek 23)(post obranca)(najvyssia_liga 2)(celkovy_vykon 74)(hrac_krajina1 Slovensko)(hrac_krajina2 Polsko)( hrac_plat 1680))
(hrac(meno Boris Cesanek)(vek 24)(post zaloznik)(najvyssia_liga 3)(celkovy_vykon 63)(hrac_krajina1 Slovensko )(hrac_krajina2 Polsko)( hrac_plat 1420))
(hrac(meno Samuel Hrabcak)(vek 26)(post obranca)(najvyssia_liga 3)(celkovy_vykon 58)(hrac_krajina1 Polsko )(hrac_krajina2 Slovensko)( hrac_plat 1150))
(hrac(meno Daniel Brejcak)(vek 25)(post zaloznik)(najvyssia_liga 3)(celkovy_vykon 49)(hrac_krajina1 Cesko )(hrac_krajina2 Polsko)( hrac_plat 1450))
(hrac(meno David Skokan)(vek 24)(post utocnik)(najvyssia_liga 3)(celkovy_vykon 68)(hrac_krajina1 Slovensko )(hrac_krajina2 Polsko)( hrac_plat 4400))
(hrac(meno Peter Bjaloncik)(vek 40)(post zaloznik)(najvyssia_liga 1)(celkovy_vykon 94)(hrac_krajina1 Slovensko)(hrac_krajina2 Polsko)( hrac_plat 4200))
(hrac(meno Matej Palocko)(vek 35)(post obranca)(najvyssia_liga 1)(celkovy_vykon 92)(hrac_krajina1 Cesko )(hrac_krajina2 Polsko)( hrac_plat 3950))
(hrac(meno Marek Berith)(vek 36)(post zaloznik)(najvyssia_liga 1)(celkovy_vykon 89)(hrac_krajina1 Polsko)(hrac_krajina2 Slovensko)( hrac_plat 4020))
(hrac(meno Martin Bilich)(vek 19)(post utocnik)(najvyssia_liga 1)(celkovy_vykon 80)(hrac_krajina1 Slovensko )(hrac_krajina2 Polsko)( hrac_plat 3200))
(hrac(meno Daniel Smuro)(vek 22)(post brankar)(najvyssia_liga 1)(celkovy_vykon 74)(hrac_krajina1 Cesko)(hrac_krajina2 Polsko)( hrac_plat 1450))
(hrac(meno Jozef Cina)(vek 29)(post zaloznik)(najvyssia_liga 2)(celkovy_vykon 67)(hrac_krajina1 Slovensko )(hrac_krajina2 Polsko)( hrac_plat 1600))
(hrac(meno Samuel Cimurak)(vek 28)(post obranca)(najvyssia_liga 3)(celkovy_vykon 66)(hrac_krajina1 Slovensko )(hrac_krajina2 Cesko)( hrac_plat 1700))
(hrac(meno Jakub Longa)(vek 20)(post zaloznik)(najvyssia_liga 3)(celkovy_vykon 65)(hrac_krajina1 Cesko )(hrac_krajina2 Slovensko)( hrac_plat 1620))
(hrac(meno Timotej Mucha)(vek 21)(post obranca)(najvyssia_liga 3)(celkovy_vykon 54)(hrac_krajina1 Cesko )(hrac_krajina2 Slovensko)( hrac_plat 980))
(hrac(meno David Chmura)(vek 35)(post zaloznik)(najvyssia_liga 3)(celkovy_vykon 57)(hrac_krajina1 Slovensko )(hrac_krajina2 Cesko)( hrac_plat 1010))
(hrac(meno Adam Chmura)(vek 36)(post zaloznik)(najvyssia_liga 2)(celkovy_vykon 69)(hrac_krajina1 Slovensko )(hrac_krajina2 Cesko)( hrac_plat 1200))
(hrac(meno Rober Grinvald)(vek 39)(post zaloznik)(najvyssia_liga 3)(celkovy_vykon 54)(hrac_krajina1 Slovensko )(hrac_krajina2 Cesko)( hrac_plat 1450))
(hrac(meno Stefan Leptech)(vek 22)(post brankar)(najvyssia_liga 3)(celkovy_vykon 48)(hrac_krajina1 Cesko )(hrac_krajina2 Slovensko)( hrac_plat 820))
(hrac(meno Martin Horvath)(vek 23)(post zaloznik)(najvyssia_liga 3)(celkovy_vykon 65)(hrac_krajina1 Polsko )(hrac_krajina2 Slovensko)( hrac_plat 1350))
(hrac(meno Adam Orszagh)(vek 18)(post zaloznik)(najvyssia_liga 2)(celkovy_vykon 75)(hrac_krajina1 Slovensko )(hrac_krajina2 Cesko)( hrac_plat 2000))
(hrac(meno Stanislav Kocisek)(vek 18)(post brankar)(najvyssia_liga 2)(celkovy_vykon 78)(hrac_krajina1 Slovensko)(hrac_krajina2 Polsko)( hrac_plat 2100))
(hrac(meno Jakub Sefcik)(vek 18)(post zaloznik)(najvyssia_liga 2)(celkovy_vykon 79)(hrac_krajina1 Slovensko)(hrac_krajina2 Polsko)( hrac_plat 2200))
(hrac(meno Denis Zaborsky)(vek 23)(post utocnik)(najvyssia_liga 1)(celkovy_vykon 82)(hrac_krajina1 Slovensko)(hrac_krajina2 Cesko)( hrac_plat 2800))
(hrac(meno Matus Jankech)(vek 23)(post utocnik)(najvyssia_liga 1)(celkovy_vykon 84)(hrac_krajina1 Cesko)(hrac_krajina2 Polsko)( hrac_plat 2900))
(hrac(meno Palo Melko)(vek 28)(post zaloznik)(najvyssia_liga 1)(celkovy_vykon 87)(hrac_krajina1 Slovensko)(hrac_krajina2 Cesko)( hrac_plat 3500))
(hrac(meno Lukas Kelemeca)(vek 23)(post zaloznik)(najvyssia_liga 1)(celkovy_vykon 90)(hrac_krajina1 Cesko )(hrac_krajina2 Slovensko)( hrac_plat 3800))
(hrac(meno Drahomir Gromovsky)(vek 35)(post brankar)(najvyssia_liga 3)(celkovy_vykon 91)(hrac_krajina1 Slovensko)(hrac_krajina2 Cesko)( hrac_plat 4500)))



(deffacts volba
(nacitana_volba))




(defrule uvodne_menu
(declare(salience 20))
?volbicka <- (nacitana_volba)
=>
	(retract ?volbicka)
	(printout t "-----------------<MENU>------------------" crlf)
	(printout t "Pre vlozenie noveho hraca stlacte pismeno a" crlf)
	(printout t "Pre vlozenie noveho timu stlacte pismeno b" crlf)
	(printout t "Pre spustenie programu stlacte pismeno c" crlf)
	(printout t "Pre ukoncenie programu stlacte pismeno d" crlf)
	(printout t "============================================" crlf)
	
	(printout t "Stlacte pismeno podla toho, ktora uloha sa ma vykonat:")
	(bind ?volba (read))
	(assert (moja_volba ?volba))
)


(defrule zlavolba
(declare(salience 20))
?zla<-(moja_volba ?x)
(not(moja_volba a))
(not(moja_volba b))
(not(moja_volba c))
(not(moja_volba d))
=>
(printout t "Zadali ste neplatnu volbu !!!!!!!!!!!!!"crlf)
(retract ?zla)
(assert (nacitana_volba))
)

(defrule udaje_noveho_hraca
	(declare(salience 19))
	(moja_volba a)
=>
	(printout t "Zadajte meno noveho hraca:" crlf)
	(bind ?noveM (readline))
	(printout t "Zadajte vek hraca:" crlf)
	(bind ?novyV (read))
	(printout t "Zadajte post hraca (brankar obranca zaloznik utocnik):" crlf)
	(bind ?novyP (read))
	(printout t "Zadajte v ktorej lige by chcel hrac hrat(1 2 3):" crlf)
	(bind ?novaL (read))
	(printout t "Zadajte celkovy vykon hraca(0-100):" crlf)
	(bind ?novyVykon (read))
	(printout t "Zadajte 1. preferovanu krajinu hraca(Slovensko Cesko Polsko su v databaze ale mohla by byt aj ina)):" crlf)
	(bind ?novaK (read))
	(printout t "Zadajte 2. preferovanu krajinu hraca (Slovensko Cesko Polsko su v databaze ale mohla by byt aj ina):" crlf)
	(bind ?novaKC (read))
	(printout t "Zadajte pozadovany plat hraca:" crlf)
	(bind ?novyPlat (read))
	(assert (hrac(meno (explode$ ?noveM))(vek ?novyV)(post ?novyP)(najvyssia_liga ?novaL)(celkovy_vykon ?novyVykon)(hrac_krajina1 ?novaK)(hrac_krajina2 ?novaKC)( hrac_plat ?novyPlat)))
	
)

(defrule zrus_volbu_a
	(declare(salience 18))
	?volbicka <- (moja_volba a)
=> 
	(retract ?volbicka)
	(assert (nacitana_volba))
)

(defrule udaje_noveho_timu
	(declare(salience 17))
	(moja_volba b)
=>
	(printout t "Zadajte nazov noveho timu:" crlf)
	(bind ?novyT (readline))
	(printout t "Zadajte ligu, v ktorej tim hra (1 2 3):" crlf)
	(bind ?novaL (read))
	(printout t "Zadajte krajinu timu(Slovensko Cesko Polsko su v databaze ale mohla by byt aj ina) :" crlf)
	(bind ?novaK (read))
	(printout t "Zadajte post, ktory tim hlada medzi hracmi (brankar obranca zaloznik utocnik):" crlf)
	(bind ?novyP (read))
	(printout t "Zadajte minimalny vek, ktory tim hlada medzi hracmi:" crlf)
	(bind ?novyVmin (read))
	(printout t "Zadajte maximalny vek, ktory tim hlada medzi hracmi:" crlf)
	(bind ?novyVmax (read))
	(printout t "Zadajte vykon, ktory tim pozaduje od hladaneho hraca (0-100) :" crlf)
	(bind ?novyVykon (read))
	(printout t "Zadajte ponukany plat pre hraca:" crlf)
	(bind ?novyPlat (read))
	(assert (tim(nazov (explode$ ?novyT))(liga ?novaL)(krajina ?novaK)(tim_poziadavka_post ?novyP)(tim_poziadavka_vek_od ?novyVmin)(tim_poziadavka_vek_do ?novyVmax)(tim_poziadavka_celkovy_vykon_od ?novyVykon)(ponukany_plat ?novyPlat)))
)

(defrule zrus_volbu_b
	(declare(salience 16))
	?volbicka <- (moja_volba b)
=> 
	(retract ?volbicka)
	(assert (nacitana_volba))
)


(defrule pockajte
(declare (salience 16))
(moja_volba c)
=>
(printout t "pockajte kym program vykona vsetky potrebne vypocty"crlf)
)


;tu by som pridal eöte asi pravila na pridania hraca a timu a moûno spravit nejake menu kde si pouzivatel vyberie co sa ma stat dal by som tam krok "pridaj hraca" "pridaj tim" a ak uz bude ;spokojny tak prikaz "prirad! ktory to vykona nizsie uvedeny vypocet a poskytne vystup (dakte v tej jej kurzoch je ako sa take menu v clipce vytvare treba pozriet) 
;pozor dolezite je aby pravidla sli poporadi preto je pri kazdom salinece o jedno niûie 
;pravidlo porovna hladane posty s postami hracov a priradi kazdemu timu hraca ktory splna post, udaje ulozi do bazy faktov ako zhoda na poste a priradi zhodu 35%
;povinna zhoda aj neplati tak sa dalej nepocita ze by bol hrac priradeny do daneho timu
(defrule porovnaj_post
(declare (salience 15))
(moja_volba c)
(tim(nazov $?q)(liga ?w)(krajina ?e)(tim_poziadavka_post ?r)(tim_poziadavka_vek_od ?t)(tim_poziadavka_vek_do ?y)(tim_poziadavka_celkovy_vykon_od ?u)(ponukany_plat ?i))
(hrac(meno $?m)(vek ?n)(post ?r)(najvyssia_liga ?v)(celkovy_vykon ?c)(hrac_krajina1 ?x)(hrac_krajina2 ?z)( hrac_plat ?l)(zhoda ?k))
=>
(bind ?k 35)
(assert (zhoda_po_poste ?q ?m ?k))
)





;pravidlo skontroluje pri hracoch ktor˝m sedeli posty ze ci ich vek sa nachadza v pozadovanom rozmedzi
;povinna zhoda aj neplati tak sa dalej nepocita ze by bol hrac priradeny do daneho timu
(defrule porovnaj_vek
(declare (salience 14))
(moja_volba c)
(tim(nazov $?q)(liga ?liga_tim)(krajina ?e)(tim_poziadavka_post ?r)(tim_poziadavka_vek_od ?t)(tim_poziadavka_vek_do ?y)(tim_poziadavka_celkovy_vykon_od ?u)(ponukany_plat ?i))
(hrac(meno $?m)(vek ?n)(post ?b)(najvyssia_liga ?liga_hrac)(celkovy_vykon ?c)(hrac_krajina1 ?x)(hrac_krajina2 ?z)( hrac_plat ?l)(zhoda ?k))
(zhoda_po_poste $?q $?m ?zapocitane_po_poste)
(test (and(>= ?n ?t)(<= ?n ?y)))
=>
(bind ?zhoda 15)
(assert (zhoda_po_veku ?q ?m (+ ?zhoda ?zapocitane_po_poste)))
)

;zmaze nepotrebne udaje o hracoch ktorym sedel post 
(defrule zmaz_stare_po_poste
(declare (salience 13))
(moja_volba c)
?zmaz<-(zhoda_po_poste $?q $?m ?zapocitane_po_poste)
=>
(retract ?zmaz)
)



;povinny_udaj ak sa nezhoduje dalsia spolupraca nepripada do uvahy mierna odchylka  je avsak mozna (konkretne max o 7 vtedy da zhodu 5percent inak 0)
(defrule porovnaj_vykon
(declare (salience 12))
(moja_volba c)
(tim(nazov $?q)(liga ?liga_tim)(krajina ?e)(tim_poziadavka_post ?r)(tim_poziadavka_vek_od ?t)(tim_poziadavka_vek_do ?y)(tim_poziadavka_celkovy_vykon_od ?u)(ponukany_plat ?i))
(hrac(meno $?m)(vek ?n)(post ?b)(najvyssia_liga ?liga_hrac)(celkovy_vykon ?c)(hrac_krajina1 ?x)(hrac_krajina2 ?z)( hrac_plat ?l)(zhoda ?k))
(zhoda_po_veku $?q $?m ?zapocitane_po_veku)
=>
(if (>= ?c ?u)
then
(bind ?zhoda 15))
(if (and(< ?c ?u)(<=(- ?u ?c)7))
then
(bind ?zhoda 5))
(if (and(< ?c ?u)(>=(- ?u ?c)8))
then
(bind ?zhoda 0))

(assert (zhoda_po_vykone ?q ?m (+ ?zhoda ?zapocitane_po_veku)))
)


;zmaze nepotrebne udaje o hracoch ktorym sedel vek alebo ich vykon bol slaby
(defrule zmaz_stare_po_veku_a_zaroven_nesediaci_vykon
(declare (salience 11))
(moja_volba c)
?zmaz<-(zhoda_po_veku $?q $?m ?rovnaka_hodnota)
?zmaz1<-(zhoda_po_vykone $?q $?m ?rovnaka_hodnota)
=>
(retract ?zmaz1)
)
;zmaze ostatne udaje o hracoch ktorym sedel vek
(defrule zmaz_stare_po_veku
(declare (salience 10))
(moja_volba c)
?zmaz<-(zhoda_po_veku $?q $?m ?zapocitane_po_veku)
=>
(retract ?zmaz)
)



;nepovinny udaj ale moze zvysit sance na zhodu
(defrule porovnaj_liga
(declare (salience 9))
(moja_volba c)
(tim(nazov $?q)(liga ?liga_tim)(krajina ?e)(tim_poziadavka_post ?r)(tim_poziadavka_vek_od ?t)(tim_poziadavka_vek_do ?y)(tim_poziadavka_celkovy_vykon_od ?u)(ponukany_plat ?i))
(hrac(meno $?m)(vek ?n)(post ?b)(najvyssia_liga ?liga_hrac)(celkovy_vykon ?c)(hrac_krajina1 ?x)(hrac_krajina2 ?z)( hrac_plat ?l)(zhoda ?k))
(zhoda_po_vykone $?q $?m ?zapocitane_po_vykone)
=>
(if (< ?liga_tim ?liga_hrac)
then
(bind ?zhoda 5))
(if (= ?liga_tim ?liga_hrac)
then
(bind ?zhoda 10))
(if (> ?liga_tim ?liga_hrac)
then
(bind ?zhoda 15))
(assert (zhoda_po_lige ?q ?m (+ ?zhoda ?zapocitane_po_vykone)))
)

(defrule zmaz_stare_po_vykone
(declare (salience 8))
(moja_volba c)
?zmaz<-(zhoda_po_vykone $?q $?m ?zapocitane_po_poste)
=>
(retract ?zmaz)
)

;pravidlo zvysujuce zhodu na zaklade krajiny ak plati prva krajina tak 12 ak druha tak 7 ak ziadna tak 0
(defrule krajina
(declare (salience 7))
(moja_volba c)
(tim(nazov $?q)(liga ?liga_tim)(krajina ?krajina_tim)(tim_poziadavka_post ?r)(tim_poziadavka_vek_od ?t)(tim_poziadavka_vek_do ?y)(tim_poziadavka_celkovy_vykon_od ?u)(ponukany_plat ?i))
(hrac(meno $?m)(vek ?n)(post ?b)(najvyssia_liga ?liga_hrac)(celkovy_vykon ?c)(hrac_krajina1 ?krajina1)(hrac_krajina2 ?krajina2)( hrac_plat ?l)(zhoda ?k))
(zhoda_po_lige $?q $?m ?zapocitane_po_lige)
=>
(if (eq ?krajina_tim ?krajina1)
then
(bind ?zhoda 12))
(if (eq ?krajina_tim ?krajina2)
then
(bind ?zhoda 7))
(if(and(neq ?krajina_tim ?krajina1)(neq ?krajina_tim ?krajina2))
then
(bind ?zhoda 0))
(assert (zhoda_po_krajine ?q ?m (+ ?zhoda ?zapocitane_po_lige)))
)

(defrule zmaz_stare_po_lige
(declare (salience 6))
(moja_volba c)
?zmaz<-(zhoda_po_lige $?q $?m ?zapocitane_po_poste)
=>
(retract ?zmaz)
)


;ak sa plat ponukany plat zhoduje s pozadovanym tak zhoda 13 ak nie ale rozdiel medzi poz.platom a pon.platom je mensi rovny 400 tak zhoda 8 inak zhoda 0
(defrule plat
(declare (salience 5))
(moja_volba c)
(tim(nazov $?q)(liga ?liga_tim)(krajina ?krajina_tim)(tim_poziadavka_post ?r)(tim_poziadavka_vek_od ?t)(tim_poziadavka_vek_do ?y)(tim_poziadavka_celkovy_vykon_od ?u)(ponukany_plat ?ponukany_plat))
(hrac(meno $?m)(vek ?n)(post ?b)(najvyssia_liga ?liga_hrac)(celkovy_vykon ?c)(hrac_krajina1 ?krajina1)(hrac_krajina2 ?krajina2)( hrac_plat ?chceny_plat)(zhoda ?k))
(zhoda_po_krajine $?q $?m ?zapocitane_po_krajine)
=>
(if (eq ?chceny_plat ?ponukany_plat)
then
(bind ?zhoda 13))
(if (and(> ?chceny_plat ?ponukany_plat)(<=(- ?chceny_plat ?ponukany_plat)400))
then
(bind ?zhoda 8))
(if (and(> ?chceny_plat ?ponukany_plat)(>=(- ?chceny_plat ?ponukany_plat)400))
then
(bind ?zhoda 0))
(if (and(< ?chceny_plat ?ponukany_plat)(<=(- ?ponukany_plat ?chceny_plat)400))
then
(bind ?zhoda 8))

(if (and(< ?chceny_plat ?ponukany_plat)(>=(- ?ponukany_plat ?chceny_plat)400))
then
(bind ?zhoda 0))
(assert (zhoda_po_plate ?q ?m (+ ?zhoda ?zapocitane_po_krajine)))
)

(defrule zmaz_stare_po_krajine
(declare (salience 4))
(moja_volba c)
?zmaz<-(zhoda_po_krajine $?q $?m ?zapocitane_po_poste)
=>
(retract ?zmaz)
)



(defrule nacitaj
(declare(salience 3))
(moja_volba c)
=>
(printout t "zadajte minimalnu zhodu pre ktoru chcete aby program vypisal priradnia hracov k timom  (60-100)")
(bind ?pov (read))
(assert (pozadovana_zhoda ?pov))
)

;vypise priradenia pozor musel som tam dat aj sablony tim a hrac aby zoznam zhoda po plate vedel ktora jeho cast patri menu timu a ktora menu hraca inak to nevedel a program crashol
(defrule vypis_priradenia
(declare (salience 2))
(moja_volba c)
(tim(nazov $?q)(liga ?liga_tim)(krajina ?e)(tim_poziadavka_post ?r)(tim_poziadavka_vek_od ?t)(tim_poziadavka_vek_do ?y)(tim_poziadavka_celkovy_vykon_od ?u)(ponukany_plat ?i))
(hrac(meno $?m)(vek ?n)(post ?b)(najvyssia_liga ?liga_hrac)(celkovy_vykon ?c)(hrac_krajina1 ?x)(hrac_krajina2 ?z)( hrac_plat ?l)(zhoda ?k))
(zhoda_po_plate $?q $?m ?zhoda)
(pozadovana_zhoda ?pov)
=>
(if (<= ?pov ?zhoda)
then
( printout t " hrac " ?m " bol priradeny do timu " ?q " so zhodou " ?zhoda"%"  crlf))
)


(defrule zrus_pozadovanu_zhodu
(declare (salience 1 ))
	?volbicka <- (pozadovana_zhoda ?pov)
=> 
	(retract ?volbicka)
)


(defrule zmaz_stare_po_plate
(declare (salience 1 ))
(moja_volba c)
?zmaz<-(zhoda_po_plate $?q $?m ?zapocitane_po_poste)
=>
(retract ?zmaz)
)


(defrule zrus_volbu_c
	?volbicka <- (moja_volba c)
=> 
	(retract ?volbicka)
	(assert (nacitana_volba))
)



(defrule ukonci_program
	(moja_volba d)
=>
	(printout t "Koniec programu. œakujeme za pozornosù! :)" crlf))























