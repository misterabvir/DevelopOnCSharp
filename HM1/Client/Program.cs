using TreeGenealogy;

FamilyFactory factory = new();

Console.WriteLine(factory.Bob.InfoFromOldestToYoungest());
/*
Bob (Male) 45 years old. (1/1/1900 - 2/2/1945) - Partner: Diana (Female) 60 years old. (2/2/1900 - 3/3/1960)
    Mike (Male) 70 years old. (6/3/1930 - 6/7/2000) - Partner: July (Female) 63 years old. (12/12/1940 - 2/6/2003)
        Tom (Male) 43 years old. (11/12/1980 - Still alive)
        Rose (Female) 48 years old. (12/12/1975 - Still alive) - Partner: Harvey (Male) 44 years old. (10/5/1979 - Still alive)
            Ema (Female) 18 years old. (5/5/2005 - Still alive)
*/

Console.WriteLine(factory.Ema.InfoFromYoungestToOldest());
/*
Ema (Female) 18 years old. (5/5/2005 - Still alive)
    Father: Harvey (Male) 44 years old. (10/5/1979 - Still alive)
    Mother: Rose (Female) 48 years old. (12/12/1975 - Still alive)
        Father: Mike (Male) 70 years old. (6/3/1930 - 6/7/2000)
            Father: Bob (Male) 45 years old. (1/1/1900 - 2/2/1945)
            Mother: Diana (Female) 60 years old. (2/2/1900 - 3/3/1960)
        Mother: July (Female) 63 years old. (12/12/1940 - 2/6/2003)
            Father: John (Male) 87 years old. (1/1/1902 - 2/2/1989)
            Mother: Marie (Female) 60 years old. (2/2/1930 - 3/3/1990)
*/
