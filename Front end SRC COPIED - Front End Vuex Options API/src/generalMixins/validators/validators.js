export default {
    methods: {
        isValidDate(format, val) {
            let errorDetails = '';
            if (val.length != 10) {
                errorDetails = ' - Lengte moet 10 zijn';
                return {
                    ok: false,
                    errorDetails
                }
            }
            let regExp = null;
            switch(format) {
                case 'YYYYMMDD': 
                    regExp = new RegExp("^[0-9]{4}-[0-9]{2}-[0-9]{2}$");
                    break;
                case 'DDMMYYYY': 
                    regExp = new RegExp("^[0-9]{2}-[0-9]{2}-[0-9]{4}$");
                    break;
            }
            const formatOK = regExp.test(val);

            if (!formatOK) {
                switch(format) {
                    case 'YYYYMMDD': 
                        errorDetails = ' - Formaat is niet YYYY-MM-DD';
                        break;
                    case 'DDMMYYYY': 
                        errorDetails = ' - Formaat is niet DD-MM-YYYY';
                        break;
                }
                return {
                    ok: false,
                    errorDetails
                }
            }

            let dd = 0;
            let mm = 0;
            let yy = 0;

            // Check dagen + maanden
            switch(format) {
                case 'YYYYMMDD': 
                    dd = +val.substring(8, 10);
                    mm = +val.substring(5, 7);
                    yy = +val.substring(0, 4);
                    break;
                case 'DDMMYYYY': 
                    dd = +val.substring(0, 2);
                    mm = +val.substring(3, 5);
                    yy = +val.substring(6, 10);
                    break;
            }

            if ( dd < 1 || dd > 31) {
                errorDetails = ' - Dag te klein of te groot';
                return {
                    ok: false,
                    errorDetails
                }
            }

            if  (mm < 1 || mm > 12)  {
                errorDetails = ' - Maand te klein of te groot';
                return {
                    ok: false,
                    errorDetails
                }
        }

            switch (mm) {
                case 4:
                case 6:
                case 9:
                case 11:
                    if (dd > 30) {
                        errorDetails = ' - Aantal dagen in de maand is te groot';
                        return {
                            ok: false,
                            errorDetails
                        }
                    }    
                    break;
                case 2:
                    if (dd > 29) {
                        errorDetails = ' - Februari kent maximaal 29 dagen';
                        return {
                            ok: false,
                            errorDetails
                        }
                    }
                    // 2004, 2008 zijn schrikkeljaren
                    // 1700, 1800, 1900 zijn geen schrikkeljaren
                    // 1600, 2000, 2400 zijn wel schrikkeljaren
                    // tweede OR operand wordt alleen geëvalueerd mits jaartal geen 400-tal is
                    const isLeapYear = (yy % 400 == 0) || 
                                    (yy % 4 == 0 && yy % 100 !=0);
                    if (dd == 29 && !isLeapYear) {
                        errorDetails = ` - ${yy} is geen schrikkeljaar`;
                        return {
                            ok: false,
                            errorDetails
                        }
                    }
                    break;
            };

            return {
                ok: true,
                errorDetails: ''
            }
        }
    }
}