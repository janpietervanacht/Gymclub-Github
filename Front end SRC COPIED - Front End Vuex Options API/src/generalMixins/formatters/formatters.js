export default {
    methods: {
        convertBooleanToString(val) {
             return val == true ? 'Ja' : 'Nee'
        },
        formatDate(formatTo, value) {
            let formatted = ''
            switch(formatTo) {
                case 'YYYYMMDD': 
                    formatted = value.substring(6, 10) + '-' + value.substring(3, 5) + '-' + value.substring(0, 2);
                    break;
                case 'DDMMYYYY': 
                    formatted = value.substring(8, 10) + '-' + value.substring(5, 7) + '-' + value.substring(0, 4);    
                    break;
            }
            return formatted;
        },
        check2DatesValid(startDate, endDate) {
            // prec: formats are both 'dd-mm-yyyy' with leading zeroes
            if (this.endDate.val == '') {
              return true;
            } else {
                let dd = +endDate.substring(0, 2);
                let mm = +endDate.substring(3, 5);
                let yy = +endDate.substring(6, 10);
                const endDateNum = dd + (mm * 100) + (yy * 10000);

                dd = +startDate.substring(0, 2);
                mm = +startDate.substring(3, 5);
                yy = +startDate.substring(6, 10);
                const startDateNum = dd + (mm * 100) + (yy * 10000);
                if (startDateNum > endDateNum) {
                    return false;
                }
            }
            return true;
        },
        makeDateFormatComplete(value) {  
            // prec: value is in d-m-yyyy format
            // Add leading zeroes where needed: eg  '7-8-2023' becomes '07-08-2023'
            let regExp = new RegExp("^[1-9]-$");
            if (regExp.test(value.substring(0,2))) {
                value = '0' + value;
            }
            regExp = new RegExp("^-[1-9]-$");
            if (regExp.test(value.substring(2,5))) {
                value = value.substring(0, 3) + '0' + value.substring(3);
            }
            return value;
        },
        simplifyDateFormat(value) { 
            // prec: value is in dd-mm-yyyy format
            // Remove leading zeroes where needed: eg  '07-11-1967' becomes '7-11-1967'

            // Code met alleen RegExps bleek niet te werken, onderstaande is beter.

            const regExp1 = new RegExp("^0[1-9]-$");
            const regExp2 = new RegExp("^[1-9][1-9]-$");

            let i = 0;
            let result = '';
            
            if (regExp1.test(value.substring(0,3))) {
                result = value.substring(1,2) + '-';
                i = 3;
            } 

            if (regExp2.test(value.substring(0,3))) {
                result = value.substring(0,2) + '-';
                i = 3;
            }
            
            if (regExp1.test(value.substring(i, i+3))) {
                result = result + value.substring(i+1, i+2) + '-';
                i = i + 3
            } 

            if (regExp2.test(value.substring(i, i+3))) {
                result = result + value.substring(i, i+2) + '-';
                i = i + 3
            }
            
            result = result += value.substring(value.length - 4, value.length);

            return result;
        }
    }
}