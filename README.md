# ShareCircle

## Avtorji

- Andraž Arhar - Vpisna številka: 63230007
- Martin Veršnjak - Vpisna številka: 63220350

---

## Zaslonske slike

### Grafični vmesnik spletne aplikacije

#### 1. Pregled skupin
![pregled_skupin](https://github.com/user-attachments/assets/ee84d79d-a014-40cd-b819-8470061e9eda)


#### 2. Pregled stroškov, vračil in stanj v določeni skupini
![pregled_stroskov_vracil_stanja](https://github.com/user-attachments/assets/6c5473d6-8821-47ab-b02f-071a06fbc7da)


#### 3. Dodajanje stroškov
![dodajanje_stroska](https://github.com/user-attachments/assets/aab62890-f842-4897-a907-d108be44ad98)


#### 4. Dodajanje uporabnikov v skupino
![dodajanje_uporabnika_v_skupino](https://github.com/user-attachments/assets/696ae1cb-0cc0-4de6-acf6-4e5aa3699a48)


#### 5. Podrobnosti stroška
![podrobnosti_stroska](https://github.com/user-attachments/assets/9e78b4f3-e8da-4bfc-bf4c-83575232fee9)


---

## Opis delovanja sistema

ShareCircle je spletna aplikacija za upravljanje skupinskih finančnih stroškov in vračil. Sistem omogoča:

- Kreiranje, posodabljanje in brisanje skupin, kjer lahko uporabniki dodajo člane in delijo stroške med seboj.
- Dodajanje vračil, stroškov in avtomatsko razdelitev stroškov na podlagi plačnika in članov skupine.
- Uporabniški vmesnik za pregled stroškov, vračil, stanj in članov skupin.
- Možnost brisanja stroškov in vračil

---

## Prispevek študentov

- **Andraž Arhar**:

  - Načrt podatkovnega modela aplikacije, njegova implementacija v model razrede
  - Skripta za polnjenje prazne baze
  - Implementacija avtorizacije za spletno aplikacijo
  - Implementacija API v spletno aplikacijo in avtorizacijo zanjo
  - Razvoj android odjemalca

- **Martin Veršnjak**:

  - Načrt podatkovnega modela aplikacije
  - Oblikovanje grafičnega vmesnika spletne aplikacije.
  - Poslovna logika spletne aplikacije
  - Razhroščevanje

---

## Podatkovni model
![database_diagram](https://github.com/user-attachments/assets/98c5edf9-1e26-4231-8d68-b5e35cbdd099)

### Opis podatkovnega modela:

1. **AspNetUsers**:

   - Vsebuje podatke o uporabnikih (ime, priimek, email, itd.).

2. **Skupina**:

   - Predstavlja skupino uporabnikov z določenim imenom in datumom nastanka.

3. **ClanSkupine**:

   - Povezovalna tabela med uporabniki in skupinami.
   - Vsebuje informacije o stanju članstva.

4. **Strošek**:

   - Beleži stroške, vključno z zneskom, plačnikom in pripadajočo skupino.

5. **RazdelitevStroska**:

   - Beleži posamezne deleže stroška, dodeljene dolžnikom.

6. **Vračilo**:

   - Evidentira vračila med uporabniki z določenim zneskom in datumom.

## Android aplikacija

  - Android aplikacija nam omogoča, da s pomočjo API-ja pregledujemo in dodajamo skupine.
![Android pregled skupin](https://github.com/user-attachments/assets/6e8c7927-074e-4968-b3c8-1d30182cd60a)
![Android dodajanje skupine](https://github.com/user-attachments/assets/243758c8-d342-47a9-a4c9-0585b7923546)
