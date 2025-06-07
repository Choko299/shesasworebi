using System;

namespace MyProject // !!! დარწმუნდი, რომ ეს namespace ზუსტად ემთხვევა შენი პროექტის სახელს (მაგ. 'MyProject') !!!
{
    // !!! გამოცდაზე: შეცვალე 'MyObject' გამოცდაზე მოცემული ობიექტის სახელით (მაგ. Car, Movie, Book)
    public class MyObject
    {
        public int Id { get; set; }

        // !!! გამოცდაზე: შეცვალე 'Property1' და მისი ტიპი (string/int/decimal), მაგ. 'Model', 'Title'
        //    'string' ტიპი საშუალებას გაძლევს შეიყვანო როგორც ასოები, ისე რიცხვები ერთად.
        public string Property1 { get; set; }

        // !!! გამოცდაზე: შეცვალე 'Property2' და მისი ტიპი, მაგ. 'ManufactureYear', 'ReleaseYear'
        //    'string' ტიპი საშუალებას გაძლევს შეიყვანო როგორც ასოები, ისე რიცხვები ერთად.
        public string Property2 { get; set; }

        // !!! გამოცდაზე: შეცვალე 'Property3' და მისი ტიპი, მაგ. 'Color', 'Director'
        //    'string' ტიპი საშუალებას გაძლევს შეიყვანო როგორც ასოები, ისე რიცხვები ერთად.
        //    ეს ველი შეიძლება იყოს ცარიელი.
        public string Property3 { get; set; }

        // !!! გამოცდაზე: შეცვალე 'Property4' და მისი ტიპი, მაგ. 'Price', 'Rating'
        //    'string' ტიპი საშუალებას გაძლევს შეიყვანო როგორც ასოები, ისე რიცხვები ერთად.
        //    ეს ველი შეიძლება იყოს ცარიელი.
        public string Property4 { get; set; }

        // !!! გამოცდაზე: თუ გამოცდაზე მეტი თვისება გჭირდება, დაამატე აქ:
        // public string Property5 { get; set; }
        // public string Property6 { get; set; }


        // ეს მეთოდი განსაზღვრავს, თუ როგორ გამოჩნდება შენი ობიექტი ListBox-ში.
        public override string ToString()
        {
            // Property1 და Property2 არის სავალდებულო და გამოჩნდება ფრჩხილებში.
            // Property3 და Property4 არის არასავალდებულო, ამიტომ 'N/A' გამოჩნდება, თუ ცარიელია.
            string prop3Display = string.IsNullOrWhiteSpace(Property3) ? "N/A" : Property3;
            string prop4Display = string.IsNullOrWhiteSpace(Property4) ? "N/A" : Property4;

            return $"{Property1} ({Property2}), დეტალი 3: {prop3Display}, დეტალი 4: {prop4Display}";
        }
    }
}
