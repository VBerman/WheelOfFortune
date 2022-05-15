using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfFortune.Shared.ViewModel
{
    public class SearchModel
    {
        private bool? _hasParking;

        public bool? HasParking
        {
            get { return _hasParking; }
            set { _hasParking = value; }
        }

        private decimal? _minArea;
        [Range(0, 10000.00)]
        public decimal? MinArea
        {
            get { return _minArea; }
            set
            {
                _minArea = value;
            }
        }
        private decimal? _maxArea;

        [Range(0, 10000.00)]
        public decimal? MaxArea
        {
            get { return _maxArea; }
            set
            {
                _maxArea = value;
            }
        }

        private decimal? _minPrice;
        [Range(0, 100000000.00)]
        public decimal? MinPrice
        {
            get { return _minPrice; }
            set
            {
                _minPrice = value;
            }
        }

        private decimal? _maxPrice;
        [Range(0, 100000000.00)]
        public decimal? MaxPrice
        {
            get { return _maxPrice; }
            set
            {
                _maxPrice = value;
            }
        }

    }
}
