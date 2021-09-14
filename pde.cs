namespace continuity
{
    // Contains the info that characterize the model
    public class pde
    {
        private double _alpha;
        private double _beta;
        private utils.Function _ufunc;
        private utils.Function _sigma;
        
        private bool incompressible;

        public double alpha { get {return _alpha;}}
        public double beta { get {return _beta;}}
        public utils.Function u { get {return _ufunc;}}
        public utils.Function sigma { get {return _sigma;}}
        public bool is_compressible { get {return !incompressible;} set {incompressible = !value;}}

        public pde(double al, double be, utils.Function uf, utils.Function sig)
        {
            incompressible=true;
            _alpha=al;
            _beta=be;
            _ufunc=uf;
            _sigma=sig;
        }

    }
}