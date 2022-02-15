import { User } from "oidc-client";
import { Category } from "src/shared/category.model";

export interface BlogPost {
    id: number;
    title: string;
    content: string;
    timeCreated: string;
    link: string;
    featuredImage: string;
    categories: Category[] | null;
    author: User;
}