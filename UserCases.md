
# Wyświetanie menu głównego z możliwością wyboru opcji myszą lub klawiaturą
## Nowa gra
### Wyświetlenie menu umożliwiającego utworzenie nowej postaci, którą gracz gbędzie grał z możliwością wyboru opcji myszą lub klawiaturą
 - Wybór rasy gracza z listy, rasa gracza definiuje początkowe statystyki
    - Człowiek - otrzymuje bonus do szybkości
    - Ork - otrzymuje bonus do siły
    - Krasnal - otrzymuje bonus do wytrzymałości
    - Elf - otrzymuje bonus do many
 - Wpisanie klawiaturą dowolnego imienia
 - Ustawienie statystyk
    - Siła
    - Mana
    - Wytrzymałość
    - Szybkość
 - Przycisk 'Potwierdź'
 - Przycisk 'Cofnij do menu'
### Wygenerowanie grafu pomieszczeń wraz z ich typami, po których gracz będzie się poruszać
  - #### Typy pomieszczeń:
    - Startowy (pusty)
    - Normalny (z normalnymi przeciwnikami)
    - Z Bossem (super silny przeciwnik)
    - Ze skarbem (ze skarbem)
    - Z kapliczką (z kapliczką umożliwiającą zakup i sprzedaż przedmiotów)
### Wyświetlenie opisu i celu gry w okienku z jednym przyciskiem, które można zamknąć klikacjąc na przycisk myszą
### Oddanie kontroli graczowi
- Poruszanie się po lochu za pomocą klawiatury lub myszki 
- #### Ekwipunek (plecak z przedmiotami)
- Okienko, w którym można przejrzeć zdobyte przedmioty, można je użyć, założyć, wyrzucić lub sprzedać/wymienić w kapliczce
- Wsyzstkie powyższe akcje posiadają odpowiednie przyciski obok przedmiotów, które się naciska myszą
- Ekwipunek można zamknąć stosownym przyciskiem
- #### Walka z przeciwnikami
  - Gracz na zmianę ze sztuczną inteligencją wykonują jeden z poniższych ruchów, kolejność wykonywania ruchów wyznaczana jest z szybkości jednostek (od najwyższej), celem danego ruchu może być dowolna jednostka na planszy walki
    - Atak - postać wykonuje cios swoją bronią na innej postaci, obrażenia jakie zadaje są zależne od statystyki siły oraz używanej broni
    - Użycie czaru - postać rzuca czar na inną postać (defensywny lub ofensywny), jego siła jest zależna od statystyki many oraz samego czaru
    - Użycie przedmiotu - postać używa przedmiotu z ekwipunku na dowolnej postaci
  - Wygrana
    - Możliwość przejścia do kolejnego pokoju, wchodząc w odpowiednie przejście w ścianie (pojawiają się one w miejscach, w których są krawędzie w grafie pomieszczeń)
    - Po pokonaniu przeciwnika przez postać gracza pojawia się okienko, w którym są dwa przyciski
       - Werbunek - pozwala zwerbować pokonanego przeciwnika do swojej armii, jeśli nie jest ona pełna
       - Wchłonięcie - wchłania duszę poległego przeciwnika, zwiększając doświadczenie postaci gracza
       - Poza tym drużyna otrzymuje złoto
    - Jeśli przeciwnika pokona jednostka wcześniej zwerbowana, dostaje ona doświadczenie oraz złoto dla drużyny
    - Po powrocie do pomieszczenia, jest ono puste
  - Przegrana
    - Wyświetlenie informacji o porażce, jest to okienko z przyciskiem "Powrót do menu głównego", które można nacisnąc myszką
## wczytaj grę
## opcje
## wyjście
