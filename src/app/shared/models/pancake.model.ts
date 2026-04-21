/* ─── Pancake Menu Item ─── */
export interface PancakeItem {
  id: number;
  name: string;
  description: string;
  price: number;
  rating: number;
  reviewCount: number;
  imageUrl: string;
  badge?: string; // e.g. 'Bestseller' | 'Chef\'s Pick' | 'New' | 'Limited'
}

/* ─── Configurator ─── */
export interface SizeOption {
  id: string;
  label: string;
  count: number;
  price: number;
}

export interface Topping {
  id: string;
  label: string;
}

export interface SyrupOption {
  id: string;
  label: string;
}

export interface AddOn {
  id: string;
  label: string;
  price: number;
}

export interface CustomOrder {
  size: SizeOption;
  toppings: Topping[];
  syrup: SyrupOption;
  addons: AddOn[];
}

/* ─── Review ─── */
export interface Review {
  id: number;
  name: string;
  role: string;
  avatarUrl: string;
  rating: number;
  text: string;
  verified?: boolean;
  featured?: boolean;
}

/* ─── Brand Metric ─── */
export interface BrandMetric {
  value: string;
  suffix: string;
  label: string;
}

/* ─── Value Proposition Card ─── */
export interface ValueCard {
  icon: string; // SVG path data
  title: string;
  description: string;
}

/* ─── Cart Item ─── */
export interface CartItem {
  name: string;
  price: number;
  quantity: number;
}
