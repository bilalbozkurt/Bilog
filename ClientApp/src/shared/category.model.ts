import { BlogPost } from "src/app/post-list/post-model";

export interface Category {
    id: number;
    name: string;
    timeCreated: string;
    blogPosts: BlogPost[] | null;
    link: string;
}